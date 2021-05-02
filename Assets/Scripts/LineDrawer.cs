using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LineDrawer : MonoBehaviour
{
    Vector3 initialMousePos;
    LineRenderer lineRenderer;
    AudioSource audioSource;

    [SerializeField]
    List<LineRenderer> amidaRendererList = new List<LineRenderer>();
    [SerializeField]
    List<GameObject> crossMarkers = new List<GameObject>();
    private int[] crossIdxList = new int[2];
    [SerializeField]
    GameObject newDrawedLine;

    [SerializeField]
    AudioClip drawSE;
    
    public List<LineStatus> newLines = new List<LineStatus>();

    public bool cantCreateLine = false;

    public int lineLimit
    {
        get;
        private set;
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lineRenderer = GetComponent<LineRenderer>();

        float maxY = (amidaRendererList[0].transform.position + amidaRendererList[0].GetPosition(0)).y;
        float minY = (amidaRendererList[0].transform.position + amidaRendererList[0].GetPosition(1)).y;
        for (int i = 0; i < amidaRendererList.Count - 1; i++)
        {
            float startY = Random.Range(minY + 1.3f, maxY - 0.3f);
            float normRand = GetRand();
            float endY = Mathf.Clamp(startY + normRand, minY + 0.3f, maxY - 0.3f);
            float startX = (amidaRendererList[i].transform.position + amidaRendererList[i].GetPosition(0)).x;
            float endX = (amidaRendererList[i + 1].transform.position + amidaRendererList[i + 1].GetPosition(0)).x;

            CreateNewLine(i, new Vector2(startX, startY), i+1, new Vector2(endX, endY));
        }
    }

    public void ResetLineLimit(int limit)
    {
        lineLimit = limit;
        cantCreateLine = false;
    }

    float GetRand()
    {
        var rndX = Random.Range(0f, 1f);
        var rndY = Random.Range(0f, 1f);

        return Mathf.Sqrt(-2.0f * Mathf.Log(rndX)) * Mathf.Cos(2.0f * Mathf.PI * rndY);
    }

    // Update is called once per frame
    void Update()
    {
        if (cantCreateLine)
        {
            lineRenderer.enabled = false;
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);
            return;
        }

        if (Input.GetMouseButtonDown(0) && lineLimit > 0) // ?}?E?X???N???b?N??????????????
        {

            initialMousePos = Input.mousePosition;
            initialMousePos = Camera.main.ScreenToWorldPoint(initialMousePos);
            transform.position = (Vector2)initialMousePos;
            lineRenderer.enabled = true;
        }
        if (Input.GetMouseButton(0) && lineLimit > 0) // ?}?E?X??????????????????
        {
            // ???_?}?[?J?[?????Z?b?g
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);

            var mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); // ???????W???????[???h???W??????
            var linePos = mousePos - initialMousePos; // ???????W?????}?E?X???W?????x?N?g??
            lineRenderer.SetPosition(1, linePos);

            float lineVecSign = Mathf.Sign(linePos.x); // ????????(???????E??????????????????????)

            // ??????????????????????2?{???????????C????????
            var nearLineList = amidaRendererList.Where(l => lineVecSign * l.transform.position.x >= lineVecSign * initialMousePos.x).
                                                Where(l => lineVecSign * l.transform.position.x <= lineVecSign * mousePos.x).
                                                OrderBy(l => Mathf.Abs(l.transform.position.x - initialMousePos.x)).Take(2);

            // ???????????_???????????_?}?[?J?[???z?u
            for (int i = 0; i < nearLineList.Count(); i++)
            {
                crossIdxList[i] = amidaRendererList.IndexOf(nearLineList.ToList()[i]);

                var l = nearLineList.ToList()[i];
                var amidaX = l.transform.position.x;
                var amidaMaxY = l.GetPosition(0).y;
                var amidaMinY = l.GetPosition(1).y;
                var crossY = linePos.y / linePos.x * (amidaX - initialMousePos.x) + initialMousePos.y; // ???_??y???W????
                if (crossY >= amidaMinY && crossY <= amidaMaxY)
                {
                    crossMarkers[i].SetActive(true);
                    crossMarkers[i].transform.position = new Vector3(amidaX, crossY, -1);
                }
            }

        }
        else if (Input.GetMouseButtonUp(0) && lineLimit > 0) // ?}?E?X??????????????????
        {
            lineRenderer.enabled = false; // ??????????
            if (crossMarkers[0].activeInHierarchy && crossMarkers[1].activeInHierarchy)
            {
                CreateNewLine(crossIdxList[0], crossMarkers[0].transform.position, crossIdxList[1], crossMarkers[1].transform.position);
                lineLimit -= 1;
                audioSource.PlayOneShot(drawSE);
            }

            // ???_?}?[?J?[?????Z?b?g
            crossMarkers[0].SetActive(false);
            crossMarkers[1].SetActive(false);
        }

    }

    void CreateNewLine(int startIdx, Vector2 startPosition, int endIdx, Vector2 endPosition)
    {
        // ?V?????????????????`??
        GameObject newLine = Instantiate(newDrawedLine);
        newLine.transform.position = startPosition;
        var l = newLine.GetComponent<LineRenderer>();
        l.SetPositions(new Vector3[2] { Vector3.zero, endPosition - startPosition });

        LineStatus ls = new LineStatus(amidaRendererList.Count);
        ls.lineObject = newLine;
        ls.isVertexLineCrossed[startIdx] = true;
        ls.isVertexLineCrossed[endIdx] = true;
        ls.crossPos[startIdx] = startPosition;
        ls.crossPos[endIdx] = endPosition;
        newLines.Add(ls);
        
    }
    
}

[System.Serializable]
public class LineStatus
{
    public GameObject lineObject;
    public bool[] isVertexLineCrossed;
    public Vector2[] crossPos;

    public LineStatus(int amidaLength)
    {
        isVertexLineCrossed = new bool[amidaLength];
        crossPos = new Vector2[amidaLength];
        for(int i = 0; i < amidaLength; i++)
        {
            isVertexLineCrossed[i] = false;
            crossPos[i] = Vector2.zero;
        }
    }
}

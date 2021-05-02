using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OfuMover : MonoBehaviour
{
    public List<GameObject> ofu = new List<GameObject>();
    public LineDrawer lineDrawer;
    public float ofuGoalY;
    public List<int> ofuStartIdxList = new List<int>();
    private List<List<Vector2>> ofuRoads = new List<List<Vector2>>();
    private bool ofuGo = false;
    private List<float> ratios = new List<float>();
    private List<int> ofuRoadindexList = new List<int>();

    [SerializeField]
    float moveSpeed = 0.5f;

    [SerializeField]
    Sprite normalFuSprite;

    [SerializeField]
    Sprite failedFuSprite;

    [SerializeField]
    KoiChecker koiChecker;

    public List<bool> ofuStop = new List<bool>();
    private List<int> ofuGoalList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetOfuMover()
    {
        ofuStop = Enumerable.Repeat(false, ofu.Count).ToList();
        ofuRoads = new List<List<Vector2>>();
        ofuRoadindexList = new List<int>();
        ofuGoalList = new List<int>();
        ratios = new List<float>();
        for (int i = 0; i < ofu.Count; i++)
        {
            ofu[i].GetComponent<SpriteRenderer>().sprite = normalFuSprite;
            ofuRoads.Add(new List<Vector2>());
            ofuRoadindexList.Add(0);
            ratios.Add(0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ofuGo)
        {

            for(int i = 0; i < ofu.Count; i++)
            {
                if (ofuStop[i])
                {
                    continue;
                }

                if (ofuRoadindexList[i] + 1 >= ofuRoads[i].Count)
                {
                    ofuStop[i] = true;

                    if (!koiChecker.CheckFu(ofuGoalList[i]))
                    {
                        ofu[i].GetComponent<SpriteRenderer>().sprite = failedFuSprite;

                    }
                    continue;
                }
                Vector2 previousPosition = ofuRoads[i][ofuRoadindexList[i]];
                Vector2 nextPosition = ofuRoads[i][ofuRoadindexList[i]+1];
                Vector2 currentPosition = Vector2.Lerp(previousPosition, nextPosition, ratios[i]);
                Vector3 movePosition = new Vector3(currentPosition.x, currentPosition.y, -2);
                ofu[i].transform.position = movePosition;
                ofu[i].transform.Rotate(0f, 0f, 3f);

                ratios[i] += moveSpeed / (nextPosition - previousPosition).magnitude * Time.deltaTime;
                if(ratios[i] >= 1.0f)
                {
                    ratios[i] = 0.0f;
                    ofuRoadindexList[i] += 1;
                }
            }

            if(ofuStop.All(x => x))
            {
                ofuGo = false;
            }
        }
    }

    public void GoOfu()
    {
        for(int i = 0; i < ofu.Count; i++)
        {
            List<Vector2> ofuRoad = new List<Vector2>();
            ofuRoad.Add(ofu[i].transform.position);
            float currentOfuY = ofu[i].transform.position.y;
            int currentIdx = ofuStartIdxList[i];

            while (true)
            {
                var lines = lineDrawer.newLines;
                var availableLines = lines.Where(l => l.isVertexLineCrossed[currentIdx] == true)
                                        .Where(l => l.crossPos[currentIdx].y < currentOfuY)
                                        .OrderByDescending(l => l.crossPos[currentIdx].y)
                                        .FirstOrDefault();
                if(availableLines == null)
                {
                    break;
                }
                var transformIdx = availableLines.isVertexLineCrossed.Select((x, idx) => new { Content = x, Index = idx })
                    .Where(x => x.Content == true && x.Index != currentIdx)
                    .Select(x => x.Index)
                    .First();
                ofuRoad.Add(availableLines.crossPos[currentIdx]);
                ofuRoad.Add(availableLines.crossPos[transformIdx]);

                currentOfuY = availableLines.crossPos[transformIdx].y;
                currentIdx = transformIdx;
            }

            ofuRoad.Add(new Vector2(ofuRoad.Last().x, ofuGoalY));
            ofuGoalList.Add(currentIdx);

            //foreach(var road in ofuRoad)
            //{
            //    Debug.Log(road);
            //}

            ofuRoads[i] = ofuRoad;
        }
        ofuGo = true;
        
    }
}

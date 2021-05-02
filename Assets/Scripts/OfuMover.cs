using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OfuMover : MonoBehaviour
{
    public List<GameObject> ofu = new List<GameObject>();
    public LineDrawer lineDrawer;
    public float ofuGoalY;
    private List<int> ofuStartIdxList = new List<int>();
    private List<List<Vector2>> ofuRoads = new List<List<Vector2>>();
    private bool ofuGo = false;
    private float ratio = 0f;
    private List<int> ofuRoadindexList = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        ofuStartIdxList = new List<int>() { 0 };
        for(int i = 0; i < ofu.Count; i++)
        {
            ofuRoads.Add(new List<Vector2>());
            ofuRoadindexList.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ofuGo)
        {
            for(int i = 0; i < ofu.Count; i++)
            {
                Vector2 previousPosition = ofuRoads[i][ofuRoadindexList[i]];
                Vector2 nextPosition = ofuRoads[i][ofuRoadindexList[i]+1];
                Vector2 currentPosition = Vector2.Lerp(previousPosition, nextPosition, ratio);
                Vector3 movePosition = new Vector3(currentPosition.x, currentPosition.y, -2);
                ofu[i].transform.position = movePosition;
            }
            ratio += 0.01f;
            if (ratio >= 1.0f)
            {
                ratio = 0;
                for (int i = 0; i < ofu.Count; i++)
                {
                    ofuRoadindexList[i] += 1;
                    if(ofuRoadindexList[i]+1 >= ofuRoads[i].Count)
                    {
                        ofuGo = false;
                    }
                }
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

            foreach(var road in ofuRoad)
            {
                Debug.Log(road);
            }

            ofuRoads[i] = ofuRoad;
        }
        ofuGo = true;
        
    }
}

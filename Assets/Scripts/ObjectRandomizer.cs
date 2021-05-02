using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectRandomizer : MonoBehaviour
{
    [SerializeField]
    List<LineRenderer> amidaLineList = new List<LineRenderer>();

    [SerializeField]
    GameObject ofuPrefab;

    [SerializeField]
    GameObject koiPrefab;

    [SerializeField]
    float ofuStartY;

    [SerializeField]
    float koiPosY;

    [SerializeField]
    OfuMover ofuMover;

    [SerializeField]
    KoiChecker koiChecker;

    [SerializeField]
    TimerView timerView;

    [SerializeField]
    int createOfuCount = 1;

    private List<GameObject> currentOfus = new List<GameObject>();
    private List<GameObject> currentKois = new List<GameObject>();

    private List<bool> oldOfuSet = new List<bool>();
    private List<bool> oldKoiSet = new List<bool>();

    // Start is called before the first frame update
    void Start()
    {
        //ResetOfu();
    }

    public void ResetOfu()
    {
        createOfuCount = PlayerPrefs.GetInt("ofuLimit", 1);

        koiChecker.ResetKoiChecker();

        foreach (var ofustack in currentOfus)
        {
            ofustack.SetActive(false);
        }

        foreach (var koistack in currentKois)
        {
            koistack.SetActive(false);
        }

        List<bool> isOfuSet = new List<bool>();
        List<bool> isKoiSet = new List<bool>();
        for (int i = 0; i < amidaLineList.Count; i++)
        {
            if (i < createOfuCount)
            {
                isOfuSet.Add(true);
                isKoiSet.Add(true);
            }
            else
            {
                isOfuSet.Add(false);
                isKoiSet.Add(false);
            }
        }

        do
        {
            isOfuSet = isOfuSet.OrderBy(_ => System.Guid.NewGuid()).ToList();
            isKoiSet = isKoiSet.OrderBy(_ => System.Guid.NewGuid()).ToList();
        } while (isOfuSet.SequenceEqual(oldOfuSet) && isKoiSet.SequenceEqual(oldKoiSet));

        oldOfuSet = isOfuSet;
        oldKoiSet = isKoiSet;

        int ofuCount = 0;
        int koiCount = 0;

        for(int i = 0; i < amidaLineList.Count; i++)
        {
            if (isOfuSet[i])
            {
                float ofuPosX = (amidaLineList[i].GetPosition(0) + amidaLineList[i].transform.position).x;
                Vector3 ofuPos = new Vector3(ofuPosX, ofuStartY, -2);

                GameObject ofuInstance;
                if(ofuCount < currentOfus.Count)
                {
                    ofuInstance = currentOfus[ofuCount];
                    ofuInstance.SetActive(true);
                }
                else
                {
                    ofuInstance = Instantiate(ofuPrefab);
                    currentOfus.Add(ofuInstance);
                }
                ofuInstance.transform.position = ofuPos;

                if (ofuCount < ofuMover.ofu.Count)
                {
                    ofuMover.ofu[ofuCount] = ofuInstance;
                    ofuMover.ofuStartIdxList[ofuCount] = i;
                    ofuCount += 1;
                }
                else
                {
                    ofuMover.ofu.Add(ofuInstance);
                    ofuMover.ofuStartIdxList.Add(i);
                    ofuCount += 1;
                }
            }

            if (isKoiSet[i])
            {
                float koiPosX = (amidaLineList[i].GetPosition(0) + amidaLineList[i].transform.position).x;
                Vector3 koiPos = new Vector3(koiPosX, koiPosY, -3f);
                GameObject koiInstance;
                if (koiCount < currentKois.Count)
                {
                    koiInstance = currentKois[koiCount];
                    koiInstance.SetActive(true);
                }
                else
                {
                    koiInstance = Instantiate(koiPrefab);
                    currentKois.Add(koiInstance);
                }
                koiInstance.transform.position = koiPos;
                koiCount += 1;

                koiChecker.koi.Add(koiInstance);
                koiChecker.koiSetList.Add(i);
            }
        }

        ofuMover.ResetOfuMover();
        timerView.ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KoiChecker : MonoBehaviour
{
    public List<GameObject> koi = new List<GameObject>();

    [SerializeField]
    List<LineRenderer> amidaLineList = new List<LineRenderer>();

    [SerializeField]
    Sprite clearKoiSprite;
    [SerializeField]
    Sprite normalKoiSprite;
    [SerializeField]
    ObjectRandomizer objectRandomizer;

    public List<int> koiSetList = new List<int>();

    private List<bool> koiCatchCheck = new List<bool>();

    [SerializeField]
    OfuMover ofuMover;

    [SerializeField]
    GameObject failedPanel;

    private bool waitReset = true;

    private int score = 0;

    [SerializeField]
    ScoreView scoreView;

    // Start is called before the first frame update
    void Start()
    {
        //fuFallCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
        koiCatchCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
    }

    public void ResetKoiChecker()
    {
        foreach(var k in koi)
        {
            k.GetComponent<SpriteRenderer>().sprite = normalKoiSprite;
        }

        koi = new List<GameObject>();
        koiSetList = new List<int>();
        waitReset = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waitReset)
        {
            if (ofuMover.ofuStop.All(x => x))
            {
                if (koiSetList.All(x => koiCatchCheck[x]))
                {
                    Clear();
                }
                else
                {
                    Failed();
                }
                waitReset = true;
                koiCatchCheck = Enumerable.Repeat(false, amidaLineList.Count).ToList();
            }
        }
    }

    public void Clear()
    {
        Debug.Log("clear");
        Invoke("ClearExecute", 1.0f);
    }

    private void ClearExecute()
    {
        objectRandomizer.ResetOfu();
    }

    public void Failed()
    {
        Debug.Log("failed");
        PlayerPrefs.SetInt("ofuCount", score);

        string highScoreKey = "ofuHighScoreEasy";
        if (PlayerPrefs.GetInt("ofuLimit", 1) != 1) highScoreKey = "ofuHighScoreHard";

        if(PlayerPrefs.GetInt(highScoreKey, 0) < score)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
        }
        failedPanel.SetActive(true);
    }

    public bool CheckFu(int fuIndex)
    {
        //fuFallCheck[fuIndex] = true;
        Debug.Log(fuIndex);
        if(koiSetList.Any(x => x == fuIndex))
        {
            koiCatchCheck[fuIndex] = true;
            koi[koiSetList.IndexOf(fuIndex)].GetComponent<SpriteRenderer>().sprite = clearKoiSprite;
            score++;
            scoreView.Show(score);
            return true;
        }
        else
        {
            return false;
        }
    }
}

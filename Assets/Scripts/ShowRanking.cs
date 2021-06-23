using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    public GameObject cantUpdateRankingPanel;

    public void Start()
    {
        if(PlayerPrefs.GetInt("isCheeting", 0) == 1)
        {
            cantUpdateRankingPanel.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
    }

    public void Show()
    {
        var score = PlayerPrefs.GetInt("ofuCount", 0);

        int boardId = 0;
        if(PlayerPrefs.GetInt("ofuLimit", 1) != 1)
        {
            boardId = 1;
        }

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score, boardId);
    }
}

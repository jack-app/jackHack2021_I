using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRanking : MonoBehaviour
{
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

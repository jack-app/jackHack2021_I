using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnLockHard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("ofuHighScoreEasy", 0) >= 10)
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }

}

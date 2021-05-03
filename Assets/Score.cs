using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject score_object = null; // Textオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
        int score_num = PlayerPrefs.GetInt("ofuCount", 0);
        Text score_text = score_object.GetComponent<Text> ();
        string score_str = score_num.ToString();
        score_text.text = score_str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreView : MonoBehaviour
{
    private Text uiText;

    private void Start()
    {
        uiText = GetComponent<Text>();
    }

    public void Show(int score)
    {
        uiText.text = score.ToString();
    }
}

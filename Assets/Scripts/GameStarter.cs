using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    Text uiText;

    [SerializeField]
    ObjectRandomizer objectRandomizer;

    public void ChangeText()
    {
        uiText.text = "スタート";
        uiText.transform.Rotate(0f, 180f, 0f);
    }

    public void StartGame()
    {
        objectRandomizer.ResetOfu();
    }
}

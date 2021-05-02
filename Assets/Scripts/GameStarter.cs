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

    [SerializeField]
    AudioClip startSE;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeText()
    {
        uiText.text = "?X?^?[?g";
        uiText.transform.Rotate(0f, 180f, 0f);
    }

    public void PlayStartSE()
    {
        audioSource.PlayOneShot(startSE);
    }

    public void StartGame()
    {
        objectRandomizer.ResetOfu();
    }
}

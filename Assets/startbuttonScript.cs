using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startbuttonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //画面切り替え
    public void OnClickStartButton()
    {
        PlayerPrefs.SetInt("ofuLimit", 1);
        Invoke("moveScene", 0.2f);
    }

    public void OnClickHardStartButton()
    {
        PlayerPrefs.SetInt("ofuLimit", 2);
        Invoke("moveScene", 0.2f);
    }

    public void OnClickRetryButton()
    {
        Invoke("moveScene", 0.2f);
    }

    void moveScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}

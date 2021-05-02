using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsobikataButtonScript : MonoBehaviour
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
        Invoke("moveScene", 0.2f);
    }

    void moveScene()
    {
        SceneManager.LoadScene("AsobikataScene");
    }
}

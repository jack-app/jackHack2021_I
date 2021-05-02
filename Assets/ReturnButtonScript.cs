using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //????????????
    public void OnClickStartButton()
    {
        Invoke("moveScene", 0.2f);
    }

    void moveScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}

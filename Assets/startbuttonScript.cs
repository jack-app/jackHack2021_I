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

    //��ʐ؂�ւ�
    public void OnClickStartButton()
    {
        Invoke("moveScene", 1.0f);
    }

    void moveScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}

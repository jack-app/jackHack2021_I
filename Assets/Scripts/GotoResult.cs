using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoResult : MonoBehaviour
{
    public void GoScene()
    {
        SceneManager.LoadScene("ResultScene");
    }
}

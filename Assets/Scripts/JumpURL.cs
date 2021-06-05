using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpURL : MonoBehaviour
{
    [SerializeField]
    private string url;

    public void JumpContactURL()
    {
        Application.OpenURL(url);
    }
}

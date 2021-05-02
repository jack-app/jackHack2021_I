using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineLimitView : MonoBehaviour
{
    [SerializeField]
    LineDrawer lineDrawer;

    private Text uiText;
    private bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.GetComponent<Text>();
        uiText.text = PlayerPrefs.GetInt("ofuLimit", 1).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart)
        {
            if(lineDrawer.lineLimit > 0)
            {
                isStart = true;
            }
        }
        else
        {
            uiText.text = lineDrawer.lineLimit.ToString();
        }
    }
}

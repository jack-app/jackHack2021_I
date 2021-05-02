using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShow : MonoBehaviour
{
    public float speedX = 0.001f;
    private Color col = Color.red;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("starting");
        GameObject g = gameObject;
        Debug.Log(g.name);
        cam = gameObject.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(speedX, 0f, 0f);
        /*
        float h, s, v = 0f;
        Color.RGBToHSV(col, out h, out s, out v);
        h = h + 0.001f;
        col = Color.HSVToRGB(h, s, v);
        cam.backgroundColor = col
        */
    }

    public void Click()
    {
        Debug.Log("‰Ÿ‚·‚È");
    }
    
}

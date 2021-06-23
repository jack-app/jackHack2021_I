using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    static int count = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            count++;
            ScreenCapture.CaptureScreenshot("screenshot" + count.ToString() + ".png", 3);

        }
    }
}

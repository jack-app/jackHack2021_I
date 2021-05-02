using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofu_drop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // —Ž‰º
        transform.Translate(0, -0.02f, 0);

        if (transform.position.y < -4.0f)
        {
            Destroy(gameObject);
        }


    }
}

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
        // ????
        transform.Translate(0, -0.6f * Time.deltaTime, 0, Space.World);
        

        // ?????_????????
        int num = Random.Range(-90, 0);

        if (transform.position.y < num)
        {
            Destroy(gameObject);
        }


    }
}

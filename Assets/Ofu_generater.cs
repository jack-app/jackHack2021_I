using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ofu_generater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ofuprefab;
    float span = 0.2f;
    float delta = 0;

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(ofuprefab) as GameObject;
            float px = Random.Range(-3f, 3f);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}

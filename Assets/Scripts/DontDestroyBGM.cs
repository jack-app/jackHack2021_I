using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyBGM : MonoBehaviour
{
    static DontDestroyBGM instance = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

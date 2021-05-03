using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    private Slider slider;
    float vol;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        vol = PlayerPrefs.GetFloat("volume", 1.0f);
        AudioListener.volume = vol;
        slider.value = vol;
    }

    // Update is called once per frame
    void Update()
    {
        var val = slider.value;
        if(vol != val)
        {
            vol = val;
            AudioListener.volume = val;
            PlayerPrefs.SetFloat("volume", val);
        }
        
    }
}

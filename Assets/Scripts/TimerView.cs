using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    float initializeTimer = 5.0f;

    [SerializeField]
    OfuMover ofuMover;

    [SerializeField]
    LineDrawer lineDrawer;

    [SerializeField]
    AudioClip countSE;

    private AudioSource audioSource;

    private float currentTimer;
    private Text timerView;
    private bool timerStop = true;
    private int currentTimerInt;

    // Start is called before the first frame update
    void Start()
    {
        //ResetTimer();
        timerView = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ResetTimer()
    {
        currentTimer = initializeTimer;
        currentTimerInt = (int)initializeTimer;
        timerStop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStop)
        {
            return;
        }

        if(currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            int timerInt = Mathf.CeilToInt(currentTimer);
            if(currentTimerInt != timerInt)
            {
                audioSource.PlayOneShot(countSE);
                currentTimerInt = timerInt;
            }
            timerView.text = timerInt.ToString();
            if(currentTimer <= 0)
            {
                lineDrawer.cantCreateLine = true;
                ofuMover.GoOfu();
            }
        }
    }
}

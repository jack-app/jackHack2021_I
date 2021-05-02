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

    private float currentTimer;
    private Text timerView;
    private bool timerStop = true;

    // Start is called before the first frame update
    void Start()
    {
        //ResetTimer();
        timerView = GetComponent<Text>();
    }

    public void ResetTimer()
    {
        currentTimer = initializeTimer;
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
            timerView.text = Mathf.CeilToInt(currentTimer).ToString();
            if(currentTimer <= 0)
            {
                lineDrawer.cantCreateLine = true;
                ofuMover.GoOfu();
            }
        }
    }
}

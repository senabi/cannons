using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class stopwatch : MonoBehaviour
{
    // Start is called before the first frame update
    public bool stopwatchActive = false;
    float currentTime;
    public Text currentTimeText;
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"ss\:ff");
    }
    public void StartStopwatch()
    {
        stopwatchActive = true;
    }
    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
    public void ResetTime()
    {
        currentTime = 0;
    }
}

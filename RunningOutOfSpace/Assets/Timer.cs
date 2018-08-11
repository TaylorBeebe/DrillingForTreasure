using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timer;
    private Text timerText; 
    private float hours, minutes, seconds;

    void Start()
    {
        //3600 seconds in an hour
        timer = timer * 60;
        timerText = GetComponent<Text>();
    }

    void Update()
    {

        timer -= Time.deltaTime;

        hours = Mathf.Floor((timer % 216000) / 3600);
        minutes = Mathf.Floor((timer % 3600) / 60);
        seconds = (timer % 60);
        timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}

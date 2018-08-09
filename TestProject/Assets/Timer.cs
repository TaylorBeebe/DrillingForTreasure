using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    private Text timerText;
    private float timer;
    private float hours, minutes, seconds;

    void Start()
    {
        timerText = GetComponent<Text>();
    }

    void Update()
    {

        timer += Time.deltaTime;

        hours = Mathf.Floor((timer % 216000) / 3600);
        minutes = Mathf.Floor((timer % 3600) / 60);
        seconds = (timer % 60);
        timerText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}

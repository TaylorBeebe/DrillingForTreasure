using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public float timer;
    private Text timerText;
    private FadeController fc;
    private float minutes, seconds, milliseconds;
    private bool timerCanRun = true;

    void Start()
    {
        //3600 seconds in an hour
        timer = 15;
        timerText = GetComponent<Text>();
        fc = GetComponent<FadeController>();
    }

    void Update()
    {
        if(timer <= 0 && timerCanRun)
        {
            int fl = GameObject.Find("Player").GetComponent<CharacterController2D>().GetLevelCurrentlyOn();
            fl += 1;
            GameObject.Find("Player").GetComponent<CharacterController2D>().SetLevelCurrentlyOn(fl);
            timerCanRun = false;
            fc.LoadScene(SceneManager.GetActiveScene().name);
        } else
        {
            timer = 0;
        }
        timer -= Time.deltaTime;
        minutes = Mathf.Floor((timer % 3600) / 60);
        seconds = Mathf.Floor(timer % 60);

        milliseconds -= Time.deltaTime * 100;

        if (milliseconds < 1)
            milliseconds = 100;

        if(timerCanRun) timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}

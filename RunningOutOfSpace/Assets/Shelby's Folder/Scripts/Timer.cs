using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public float timer;
    private Text timerText;
    private FadeController fc;
    private CharacterController2D cc2d;
    private float minutes, seconds, milliseconds;
    private bool timerCanRun = true;

    void Start()
    {
        timerText = GetComponent<Text>();
        fc = GetComponent<FadeController>();
        cc2d = FindObjectOfType<CharacterController2D>();
        float newTimer = cc2d.timer + (cc2d.timer * (cc2d.timerMultiplayer * (cc2d.GetLevelCurrentlyOn() - 1)));
        timer = newTimer;
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
        }
        if (timer <= 0 && timerCanRun)
        {
            timerCanRun = false;
        } else if (!timerCanRun)
        {
            timerText.text = "00:00:00";
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

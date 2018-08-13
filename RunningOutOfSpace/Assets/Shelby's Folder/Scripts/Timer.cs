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

    void Start()
    {
        //3600 seconds in an hour
        timer = 90;
        timerText = GetComponent<Text>();
        fc = GetComponent<FadeController>();
    }

    void Update()
    {
        if(timer <= Mathf.Epsilon)
        {
            int fl = PlayerPrefs.GetInt("FloorPers");
            fl += 1;
            PlayerPrefs.SetInt("FloorPers", fl);
            fc.LoadScene(SceneManager.GetActiveScene().name);
        }
        timer -= Time.deltaTime;
        minutes = Mathf.Floor((timer % 3600) / 60);
        seconds = Mathf.Floor(timer % 60);

        milliseconds -= Time.deltaTime * 100;

        if (milliseconds < 1)
            milliseconds = 100;

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}

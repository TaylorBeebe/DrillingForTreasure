using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour {

    public float startTime = 1f;
    public float stopTime = 5f; 

    Sound s; 

	// Use this for initialization
	void Start () {
        Invoke("Play", startTime);
        Invoke("Stop", stopTime);
    }

    void Play ()
    {
        //s = FindObjectOfType<AudioManager>().StartAdLoop("AdMusic1");
        print("Commence!");
    }

    void Stop ()
    {
        //FindObjectOfType<AudioManager>().StopAdLoop(s);
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}

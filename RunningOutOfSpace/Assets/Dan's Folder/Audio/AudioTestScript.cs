using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Play", 1f);
    }

    void Play ()
    {
        FindObjectOfType<AudioManager>().Play("AdMusic1");
        print("Commence!");
    }
    

    // Update is called once per frame
    void Update () {
		
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        FindObjectOfType<AudioManager>().Play("AdMusic1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class testbutton : MonoBehaviour {
    public Button b;
	// Use this for initialization
	void Start () {
        b.onClick.AddListener(X);
	}
	
	// Update is called once per frame
	void X () {
        GetComponent<CameraShaker>().ShakeOnce(4f, 4f, 0, 0.1f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

    enum SingletonTypes {None, Camera, Canvas};
    [SerializeField] SingletonTypes type = SingletonTypes.Camera;

	// Use this for initialization
	void Awake () {
		if(type == SingletonTypes.Camera)
        {
            int amountOfCams = FindObjectsOfType<Camera>().Length;
            if (amountOfCams > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }
        switch(type)
        {
            case (SingletonTypes.Camera):
                int amountOfCams = FindObjectsOfType<Camera>().Length;
                if (amountOfCams > 1)
                    Destroy(gameObject);
                else
                    DontDestroyOnLoad(gameObject);
                return;

            case (SingletonTypes.Canvas):
                int amountOfCanvases = FindObjectsOfType<Canvas>().Length;
                if (amountOfCanvases > 1)
                    Destroy(gameObject);
                else
                    DontDestroyOnLoad(gameObject);
                return;

            default:
                return;
            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

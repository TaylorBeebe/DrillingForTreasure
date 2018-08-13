using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillHealth : MonoBehaviour {


    HealthAndVariables hav;
    float health;
	// Use this for initialization
	void Start () {
        hav = GetComponent<HealthAndVariables>();
        health = hav.health;
	}
	
	// Update is called once per frame
	void Update () {
        health = hav.health;

        if (health <= 0)
        {

            Debug.Log("GAME IS OVER, DRILL DIED");
            //Game over

        }

	}
}

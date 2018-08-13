using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {

    [SerializeField] Vector2 locChange;
    [SerializeField] float oscillationPeriod = 2f;

    float movementFactor;
    Vector2 startingPos;
    Vector2 targetLoc;
    bool canCheck;
    int drillDown = 0;

	// Use this for initialization
	void Start () {
        startingPos = (Vector2)transform.position;
        targetLoc += locChange;
	}
	
	// Update is called once per frame
	void Update () {
		if(oscillationPeriod <= Mathf.Epsilon) { return; }
        float cycles = Time.time / oscillationPeriod;

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector2 offset = targetLoc * movementFactor;
        Vector2 fixLoc = targetLoc + new Vector2(0.1f,0.1f);
        if(offset.y < fixLoc.y && canCheck)
        {

            drillDown += 1;
            //print(canCheck);
            canCheck = false;
        } else if(canCheck == false)
        {
            canCheck = true;
        }
        transform.position = startingPos + offset;
	}
}

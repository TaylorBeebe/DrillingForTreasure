using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour {

    [SerializeField] Vector2 targetLoc;
    [SerializeField] float period = 2f;

    float movementFactor;
    Vector2 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = (Vector2)transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2f;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector2 offset = targetLoc * movementFactor;
        transform.position = startingPos + offset;
	}
}

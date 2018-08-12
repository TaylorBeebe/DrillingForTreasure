using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour {
    public float distancefromDrone;
    DroneCon dc;
    public bool inRadius;
    bool completed;
	// Use this for initialization
	void Start () {
        dc = FindObjectOfType<DroneCon>();
        dc.enemies.Add(this.transform);
	}
	
	// Update is called once per frame
	void Update () {
        distancefromDrone = (dc.transform.position - this.transform.position).magnitude;
        if (distancefromDrone <= dc.enemyCheckDist)
        {
            inRadius = true;
        }
	}
}

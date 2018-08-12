using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager enemyManager;
    public List<AIBehaviour> enemies = new List<AIBehaviour>();
    public Transform target;
	// Use this for initialization
	void Start () {
        enemyManager = this;
        foreach(AIBehaviour enemy in enemies)
        {
        }
	}
	
	// Update is called once per frame
	void Update () {

        foreach (AIBehaviour enemy in enemies)
        {
            enemy.AIDestination.target = target;
        }
    }
}

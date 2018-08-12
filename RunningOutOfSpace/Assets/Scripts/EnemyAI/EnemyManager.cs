using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager enemyManager;
    public List<Enemy> enemies = new List<Enemy>();
    public Transform target;
	// Use this for initialization
	void Start () {
        enemyManager = this;
        foreach(Enemy enemy in enemies)
        {
            enemy.OnCreate();
        }
	}
	
	// Update is called once per frame
	void Update () {

        foreach (Enemy enemy in enemies)
        {
            enemy.Execute();
            enemy.AIDestination.target = target;
        }
    }
}

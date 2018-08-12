using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager enemyManager;
    public List<Enemy> enemies = new List<Enemy>();
    public Transform Player;
    public Transform Drill;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Drill = GameObject.FindGameObjectWithTag("Drill").transform;
    }
}

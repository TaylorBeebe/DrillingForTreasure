using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour {

    public AIPath aiAgent;
    public AIDestinationSetter AIDestination;
    public Transform target;
    public float health = 100f;

    public enum EnemyStates {
        attack,
        follow,
        dead
    }
    public EnemyStates enemyStates;
    public virtual void OnCreate()
    {
        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        EnemyManager.enemyManager.enemies.Add(this);

    }
    public virtual void Execute()
    {

    }
    public virtual void OnDeath(float WaitBeforeDestroying)
    {
        Destroy(this, WaitBeforeDestroying);
    }
}

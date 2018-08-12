using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class Enemy : MonoBehaviour {

    public AIPath aiAgent;
    public AIDestinationSetter AIDestination;
    public Transform target;
    public float health = 100f;
    public float distanceThresholdForAttack = 0;
    public bool canMove;
    public float DamagePerSecond;
    public bool canAttack = false;

    EnemyManager enemyManager;

    public enum EnemyStates {
        attack,
        follow,
        dead
    }
    public EnemyStates enemyStates;
    public virtual void Start()
    {
        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        enemyManager = FindObjectOfType<EnemyManager>();
        int i = Random.Range(0, 2);
        if (i == 0) target = enemyManager.Player;
        else target = enemyManager.Drill;
    
    }
    public virtual void Update()
    {
        if (health > 0 && Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)
        {
            enemyStates = EnemyStates.attack;
        }
        else
       if ((health > 0 && !(Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)))
        {
            enemyStates = EnemyStates.follow;
        }
        else
        {
            enemyStates = EnemyStates.dead;
        }

        switch (enemyStates)
        {
            case EnemyStates.follow:
                OnFollow();
                break;
            case EnemyStates.dead:
                OnDeath(2f);
                break;
            case EnemyStates.attack:
                if (canAttack)
                {
                    OnAttack();
                    Debug.Log("Attack");
                }
                break;
        }
        //aiAgent.destination = target.position;
        AIDestination.target = target;
        //aiAgent.canMove = canMove;
    }
    public virtual void OnDeath(float WaitBeforeDestroying)
    {
        Destroy(this, WaitBeforeDestroying);
    }
    public virtual void OnAttack() { }
    public virtual void OnFollow() { }
    public void DoDamage(float damage)
    {
        health -= damage;
    }
}

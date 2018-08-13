using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(HealthAndVariables))]
public class Enemy : MonoBehaviour {

    public float _attackTimer = 1f;
    public AIPath aiAgent;
    public AIDestinationSetter AIDestination;
    public Transform target;
    public float health;
    public float distanceThresholdForAttack = 0;
    public bool canMove;
    public float DamagePerSecond;
    public bool canAttack = false;
    public HealthAndVariables healthAndVariables;
    EnemyManager enemyManager;

    public enum EnemyStates {
        attack,
        follow,
        dead,
        idle
    }
    public EnemyStates enemyStates;
    public virtual void Start()
    {
        healthAndVariables = GetComponent<HealthAndVariables>();
        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        enemyManager = FindObjectOfType<EnemyManager>();
        int i = Random.Range(0, 2);
        if (i == 0) target = enemyManager.Player;
        else target = enemyManager.Drill;
    
    }
    public virtual void Update()
    {
        health = healthAndVariables.Health;

        if (health > 0 && Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)
        {
            enemyStates = EnemyStates.attack;
        }
        else
        if ((health > 0 && !(Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)))
        {
            enemyStates = EnemyStates.follow;
        }
        if(health == 0)
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
        Destroy(this.gameObject, WaitBeforeDestroying);
    }
    public virtual void OnAttack()
    {
        canAttack = false;

        Invoke("UpdateCanAttack", _attackTimer);
    }
    public virtual void OnFollow() { }

    void UpdateCanAttack()
    {
        canAttack = true;
    }
}

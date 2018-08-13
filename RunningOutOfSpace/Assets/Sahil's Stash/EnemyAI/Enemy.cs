using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(HealthAndVariables))]
public class Enemy : MonoBehaviour {

    public AIPath aiAgent;
    public AIDestinationSetter AIDestination;
    public Transform target;
    [HideInInspector] public float health = 100f;
    public float distanceThresholdForAttack = 10f;
    [HideInInspector] public bool canMove;
    //public float DamagePerSecond;
    [HideInInspector] public bool canAttack = false;

    public int damage;

    HealthAndVariables healthAndVariables; 

    EnemyManager enemyManager;

    public enum EnemyStates {
        attack,
        follow,
        dead
    }
    public EnemyStates enemyStates;
    public virtual void Start()
    {
        canMove = true;

        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        enemyManager = FindObjectOfType<EnemyManager>();
        int i = Random.Range(0, 2);
        if (i == 0) target = enemyManager.Player;
        else target = enemyManager.Drill;

        healthAndVariables = GetComponent<HealthAndVariables>();

    }
    public virtual void Update()
    {
        // Do nothing if dead
        if (enemyStates == EnemyStates.dead) return;

        // Fetch health from appropriate script
        health = healthAndVariables.health;

        if (health <= 0)
        {
            enemyStates = EnemyStates.dead;
        }
        else if (Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)
        {
            enemyStates = EnemyStates.attack;
        }
        else //if (!(Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack))
        {
            enemyStates = EnemyStates.follow;
        }

        switch (enemyStates)
        {
            case EnemyStates.follow:
                OnFollow();
                break;
            case EnemyStates.dead:
                OnDeath();
                Die();
                break;
            case EnemyStates.attack:
                if (canAttack)
                {
                    Debug.Log("Attacking " + target.name + " for " + damage + " damage"); //## where is damage stored?
                    OnAttack();
                }
                break;
        }
        //aiAgent.destination = target.position;
        AIDestination.target = target;
        //aiAgent.canMove = canMove;
    }

    void Die()
    {
        GetComponent<Collider>().isTrigger = true; // prevents further bullets from hitting it
        Destroy(gameObject, 2f);
    }

    public virtual void OnDeath() { }
    public virtual void OnAttack() { }
    public virtual void OnFollow() { }

}

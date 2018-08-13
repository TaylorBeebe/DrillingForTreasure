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

    [HideInInspector] public EnemyManager enemyManager;

    public enum EnemyStates {
        attack,
        follow,
        dead
    }
    public EnemyStates enemyStates;
    public virtual void Start()
    {
        canMove = true;
        canAttack = true;

        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        enemyManager = FindObjectOfType<EnemyManager>();

        RandomiseTarget();

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
        else if (Vector2.Distance(transform.position, target.position) <= distanceThresholdForAttack)
        {
            enemyStates = EnemyStates.attack;
            //Debug.Log("Switching to attack state");
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
                //Debug.Log(Vector2.Distance(transform.position, target.position));
                OnDeath();
                Die();
                break;
            case EnemyStates.attack:
                if (canAttack)
                {
                    //Debug.Log("Attacking " + target.name + " for " + damage + " damage");
                    OnAttack();
                }
                break;
        }
        //aiAgent.destination = target.position;
        AIDestination.target = target;
        //aiAgent.canMove = canMove;
    }

    public void Die()
    {
        GetComponent<Collider2D>().enabled = false; // prevents further bullets from hitting it
        this.canMove = false;
        Destroy(gameObject, 2f);
    }

    void RandomiseTarget()
    {
        target = (Random.Range(0, 2) == 0) ? enemyManager.Player : enemyManager.Drill;
    }

    public virtual void OnDeath() {
        enemyStates = EnemyStates.dead;
        
    }
    public virtual void OnAttack() {
    }
    public virtual void OnFollow() { }

}

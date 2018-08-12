using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIBehaviour : MonoBehaviour {
    public AIPath aiAgent;
    public AIDestinationSetter AIDestination;
    public Transform target;
    public float health = 100f;
    public float distanceThresholdForAttack = 0;
    public bool canMove;
    public float DamagePerSecond;

    EnemyManager enemyManager;

    public enum EnemyStates {
        attack,
        follow,
        dead
    }
    public EnemyStates enemyStates;

    public enum teamInfo
    {
        Player,
        Enemy
    }
    public teamInfo team;
    public virtual void Start()
    { 
        aiAgent = GetComponent<AIPath>();
        AIDestination = GetComponent<AIDestinationSetter>();
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.enemies.Add(this);
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
                OnAttack();
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

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
    AIBehaviour[] GetEnemiesofTeam(teamInfo TeamInfo)
    {
        AIBehaviour[] allAI = FindObjectsOfType<AIBehaviour>();
        List<AIBehaviour> personalVendatta = new List<AIBehaviour>(allAI.Length);

        for (int i = 0; i < allAI.Length; i++)
        {
            personalVendatta[i] = allAI[i];
        }
        foreach(AIBehaviour ai in personalVendatta)
        {
            if(ai.team != TeamInfo)
            {
                personalVendatta.Remove(ai);
            }
        }
        return personalVendatta.ToArray();
    }
    
}

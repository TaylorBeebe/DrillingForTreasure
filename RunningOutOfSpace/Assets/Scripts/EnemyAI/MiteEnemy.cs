using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteEnemy : Enemy {
    public bool canMove;
    public float distanceThresholdForAttack;
    public override void Execute()
    {
        if(health > 0 && Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)
        {
            enemyStates = EnemyStates.attack;
        }else
        if((health > 0 && !(Vector3.Distance(transform.position, target.position) <= distanceThresholdForAttack)))
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
                InvokeRepeating("MiteMove", .5f, .5f);
                break;
            case EnemyStates.dead:
                OnDeath(2f);
                break;
        }
        
    }
    public void MiteMove()
    {
        canMove = !canMove;
    }
    public void OnAttack()
    {
        canMove = false;
    }
    public override void OnDeath(float WaitBeforeDestroying)
    {
        base.OnDeath(WaitBeforeDestroying);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DemolisherEnemy : Enemy
{

    public float attackAnimationTime = 1.2f;

    public override void Start()
    {
        base.Start();
        target = enemyManager.Drill;
    }
    public override void Update()
    {
        base.Update();
        aiAgent.canMove = canMove;
        //aiAgent.canMove = true;
    }

    public override void OnFollow()
    {
        base.OnFollow();
    }

    public override void OnAttack()
    {
        canMove = false;
        canAttack = false;

        target.GetComponent<HealthAndVariables>().TakeDamage(damage);
        Invoke("UpdateCanAttack", attackAnimationTime);
    }

    public override void OnDeath()
    {
        base.OnDeath();
    }

    void UpdateCanAttack()
    {
        canAttack = true;
    }
}


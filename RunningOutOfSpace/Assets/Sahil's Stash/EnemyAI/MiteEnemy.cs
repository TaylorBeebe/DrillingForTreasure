using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MiteEnemy : Enemy {

    public float attackAnimationTime = 0.75f;
    public float timeBetweenWriggles = 1f;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("MiteMove", timeBetweenWriggles, timeBetweenWriggles);
    }
    public override void Update()
    {
        base.Update();
        
        aiAgent.canMove = canMove;
       // AIDestination.target = target;
        
    }
    public override void OnFollow()
    {
        base.OnFollow();
        //StartCoroutine(WaitAndGo());
    }
    public void MiteMove()
    {
        canMove = !canMove;
        //Debug.Log("Changed");
      //StartCoroutine(WaitAndGo());
    }
    public override void OnAttack()
    {
        canMove = false;
        canAttack = false;

        //HealthAndVariables.DoDamage(5, target); //TODO  replace with damage var 
        target.GetComponent<HealthAndVariables>().TakeDamage(damage);

        Invoke("UpdateCanAttack", attackAnimationTime);
    }

    /*
    public override void OnDeath(float WaitBeforeDestroying)
    {
        base.OnDeath(WaitBeforeDestroying);
    }
    */

    void UpdateCanAttack()
    {
        canAttack = true;
    }
}


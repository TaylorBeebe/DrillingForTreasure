using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MiteEnemy : Enemy {
  
   
    public float breakTime;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("MiteMove", breakTime, breakTime);
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
        Debug.Log("Changed");
      //StartCoroutine(WaitAndGo());
    }
    public override void OnAttack()
    {
        canMove = false;
        
    }
    public override void OnDeath(float WaitBeforeDestroying)
    {
        base.OnDeath(WaitBeforeDestroying);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ExploderEnemy : Enemy
{

    public int minMites = 3;
    public int maxMites = 5;

    public float attackAnimationTime = 0.5f;
    public GameObject mite;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        aiAgent.canMove = canMove;
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

        int miteCount = Random.Range(minMites, maxMites + 1);
        Debug.Log("Exploder died! Spawning " + miteCount + " mites!");

        for (int i = 0; i < miteCount; i++)
        {
            GameObject miteGO = Instantiate(mite);
            miteGO.transform.position = transform.position;
        }
    }

    void UpdateCanAttack()
    {
        canAttack = true;
    }
}


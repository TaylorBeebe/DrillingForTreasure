﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ExploderEnemy : Enemy
{

    public int minMites = 3;
    public int maxMites = 5;

    public float attackAnimationTime = 0.5f;
    public GameObject mite;

    bool wasHit = false;
    SpriteRenderer renderer;

    public Sprite deathSprite;
    public Sprite attackSprite;
    
    public override void Start()
    {
        base.Start();
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    public override void Update()
    {
        base.Update();
        aiAgent.canMove = canMove;
        //aiAgent.canMove = true;
    }

    public void LateUpdate()
    {
        if (wasHit)
        {
            wasHit = false;
        }
        else
        {
            renderer.color = Color.white;
        }
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
        renderer.sprite = deathSprite;
        int miteCount = Random.Range(minMites, maxMites + 1);
        //Debug.Log("Exploder died! Spawning " + miteCount + " mites!");

        for (int i = 0; i < miteCount; i++)
        {
            Invoke("Spawn", i * 0.15f);
        }
    }

    void Spawn()
    {
        Instantiate(mite, transform.position, Quaternion.identity);
    }

    void UpdateCanAttack()
    {
        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            other.GetComponent<Bullet>().Destroy(other.transform.position);
            this.GetComponent<HealthAndVariables>().TakeDamage(20);
            renderer.color = Color.red;
            wasHit = true;
        }
    }
}

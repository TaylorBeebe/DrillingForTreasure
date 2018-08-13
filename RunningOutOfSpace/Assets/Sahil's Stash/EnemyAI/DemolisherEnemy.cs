using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;

public class DemolisherEnemy : Enemy
{

    public float attackAnimationTime = 1.2f;
    bool wasHit = false;
    SpriteRenderer renderer;
    public Sprite deathSprite;
    public Sprite attackSprite;
    public Sprite aliveSprite;

    SortingGroup group;

    public override void Start()
    {
        base.Start();
        group = GetComponent<SortingGroup>();
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        target = enemyManager.Drill;
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
        AttackAnimation();
        target.GetComponent<HealthAndVariables>().TakeDamage(damage);
        Invoke("UpdateCanAttack", attackAnimationTime);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        renderer.sprite = deathSprite;
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
            GetComponent<HealthAndVariables>().TakeDamage(20);
            renderer.color = Color.red;
            wasHit = true;
        }
    }

    void AttackAnimation()
    {
        renderer.sprite = attackSprite;
        Invoke("NormalAnimation", 1f);
    }

    void NormalAnimation()
    {
        renderer.sprite = aliveSprite;
    }
}


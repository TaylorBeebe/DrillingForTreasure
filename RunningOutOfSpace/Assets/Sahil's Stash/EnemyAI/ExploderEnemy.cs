using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ExploderEnemy : Enemy {

    public int minMites = 3;
    public int maxMites = 5;
    public float timeBetweenAttacks = 0.5f;
    public GameObject mite;
    bool wasHit = false;
    SpriteRenderer renderer;
    public Sprite deathSprite;
    public Sprite attackSprite;
    public Sprite aliveSprite;
    
    public override void Start()
    {
        base.Start();
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        canAttack = true;
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
        if (canAttack)
        {
            canAttack = false;
            AttackAnimation();
            //HealthAndVariables.DoDamage(5, target); //TODO  replace with damage var 
            target.GetComponent<HealthAndVariables>().TakeDamage(damage);
            Debug.Log("Exploder Dealing Damage to Player");
            Invoke("UpdateCanAttack", timeBetweenAttacks);
        }
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


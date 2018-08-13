using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpitterV2 : Enemy
{

    public float timeBetweenAttacks = 0.75f;
    bool wasHit = false;
    SpriteRenderer renderer;
    public Sprite deathSprite;
    public Sprite attackSprite;
    public Sprite aliveSprite;
    public GameObject spit;
    public Transform bulletSpawn;
    public override void Start()
    {
        base.Start();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    public override void Update()
    {
        base.Update();

        aiAgent.canMove = canMove;
        // AIDestination.target = target;

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
        //StartCoroutine(WaitAndGo());
    }
    public override void OnAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            canMove = false;
            AttackAnimation();
            //HealthAndVariables.DoDamage(5, target); //TODO  replace with damage var 
            GameObject obj = Instantiate(spit,transform.position,Quaternion.identity);
            obj.AddComponent<Rigidbody2D>().velocity = (target.position - bulletSpawn.position) * 10f;
            obj.transform.rotation = Quaternion.LookRotation(obj.AddComponent<Rigidbody2D>().velocity);
            Debug.Log("Mite Dealing Damage to Player");
            Invoke("UpdateCanAttack", timeBetweenAttacks);
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();
        CancelInvoke();
        renderer.sprite = deathSprite;
        //Debug.Log("Mite Died");
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

    void AttackAnimation()
    {
        renderer.sprite = attackSprite;
        Invoke("NormalAnimation", 0.5f);
    }

    void NormalAnimation()
    {
        renderer.sprite = aliveSprite;
    }

}


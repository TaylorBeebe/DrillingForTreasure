using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MiteEnemy : Enemy {

    public float timeBetweenAttacks = 0.75f;
    public float timeBetweenWriggles = 1f;
    bool wasHit = false;
    SpriteRenderer renderer;
    public Sprite deathSprite;
    public Sprite attackSprite;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("MiteMove", timeBetweenWriggles, timeBetweenWriggles);
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    public override void Update()
    {
        base.Update();
        
        aiAgent.canMove = canMove;
       // AIDestination.target = target;
        
    }

    public void LateUpdate() {
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
    public void MiteMove()
    {
        canMove = !canMove;
        //Debug.Log("Changed");
        //StartCoroutine(WaitAndGo());
    }
    public override void OnAttack()
    {
        if (canAttack)
        {
            //canMove = false;
            canAttack = false;

            //HealthAndVariables.DoDamage(5, target); //TODO  replace with damage var 
            target.GetComponent<HealthAndVariables>().TakeDamage(damage);
            Debug.Log("Mite Dealing Damage to Player");
            Invoke("UpdateCanAttack", timeBetweenAttacks);
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();
        CancelInvoke();
        renderer.sprite = deathSprite;
        Debug.Log("Mite Died");
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


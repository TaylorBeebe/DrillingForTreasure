using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MiteEnemy : Enemy {

    public float attackAnimationTime = 0.75f;
    public float timeBetweenWriggles = 1f;
    bool wasHit = false;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("MiteMove", timeBetweenWriggles, timeBetweenWriggles);
    }
    public override void Update()
    {
        base.Update();
        /*
        if (wasHit) {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255);
            wasHit = false;
        }
        */
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
    public override void OnDeath()
    {
        base.OnDeath();
        CancelInvoke();
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
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(245, 159, 159);
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            wasHit = true;
        }
    }
}


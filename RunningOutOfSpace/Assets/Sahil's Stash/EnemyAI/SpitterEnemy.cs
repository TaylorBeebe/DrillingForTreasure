using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpitterEnemy : Enemy
{

    public Vector3 toTarget; //remove this up here;
    public float zDegs; //remove

    public GameObject spittle;
    public float attackAnimationTime = 0.35f;

    [SerializeField] float spitSpeed = 10f;
    [SerializeField] float spitDespawnTime = 4f;

    public override void Start()
    {
        base.Start();
        target = enemyManager.Player;
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
        base.OnAttack();

        //if (!canAttack) return;

        Spit();
        //target.GetComponent<HealthAndVariables>().TakeDamage(damage);
        Invoke("UpdateCanAttack", attackAnimationTime);

        canMove = false;
        canAttack = false;

    }

    void Spit()
    {
        
        Vector2 spawnPoint = transform.position;
        Vector2 targetPoint = target.transform.position;
        /*Vector2*/ toTarget = targetPoint - spawnPoint;
        Vector2 thirdPoint = new Vector2(targetPoint.x, spawnPoint.y);

        /*
        Vector3 spawnPoint = transform.position;
        Vector3 targetPoint = target.transform.position;
        /*Vector3// toTarget = targetPoint - spawnPoint;
        */

        float hyp = Vector2.Distance(spawnPoint, targetPoint);
        float opp = Vector2.Distance(thirdPoint, targetPoint);

        float zRads = Mathf.Asin(opp / hyp);
        /*float*/
        zDegs = zRads * Mathf.Rad2Deg;

        // Cast it
        if (targetPoint.x <= spawnPoint.x && targetPoint.y >= spawnPoint.y) zDegs = 90 - zDegs;
        else if (targetPoint.x <= spawnPoint.x && targetPoint.y <= spawnPoint.y) zDegs += 90;
        else if (targetPoint.x >= spawnPoint.x && targetPoint.y <= spawnPoint.y) zDegs = 90f - (zDegs + 180);//zDegs += 180;
        else if (targetPoint.x >= spawnPoint.x && targetPoint.y >= spawnPoint.y) zDegs += 270;



        ////GameObject go = Instantiate(spittle, spawnPoint, Quaternion.LookRotation(new Vector3(toTarget.x, 0f, 0f)));
        GameObject go = Instantiate(spittle, spawnPoint, Quaternion.Euler(0, 0, zDegs)); //Quaternion.Euler(toTarget.x, 0, 1));  
        //GameObject go = Instantiate(spittle, spawnPoint, Quaternion.LookRotation(new Vector3(toTarget.x, 0f, 0f)));
        go.GetComponent<Spittle>().InheritValues(damage, spitSpeed, spitDespawnTime);
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


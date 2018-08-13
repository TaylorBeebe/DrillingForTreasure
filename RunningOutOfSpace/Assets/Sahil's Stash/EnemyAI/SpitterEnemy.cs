using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpitterEnemy : Enemy
{

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
        float zDir = GetShootAngle(spawnPoint, target.transform.position);
        ////GameObject go = Instantiate(spittle, spawnPoint, Quaternion.LookRotation(new Vector3(toTarget.x, 0f, 0f)));
        GameObject go = Instantiate(spittle, spawnPoint, Quaternion.Euler(0, 0, zDir)); //Quaternion.Euler(toTarget.x, 0, 1));  
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
        canMove = true;
    }
    float GetShootAngle(Vector2 startPoint, Vector2 endPoint)
    {
        Vector2 toTarget = endPoint - startPoint;
        Vector2 thirdPoint = new Vector2(endPoint.x, startPoint.y);

        /*
        Vector3 startPoint = transform.position;
        Vector3 endPoint = target.transform.position;
        /*Vector3// toTarget = endPoint - startPoint;
        */

        float hyp = Vector2.Distance(startPoint, endPoint);
        float opp = Vector2.Distance(thirdPoint, endPoint);

        float zRads = Mathf.Asin(opp / hyp);
        /*float*/
        float zDegs = zRads * Mathf.Rad2Deg;

        // Cast it
        if (endPoint.x <= startPoint.x && endPoint.y >= startPoint.y) zDegs = 90 - zDegs;
        else if (endPoint.x <= startPoint.x && endPoint.y <= startPoint.y) zDegs += 90;
        else if (endPoint.x >= startPoint.x && endPoint.y <= startPoint.y) zDegs = 90f - (zDegs + 180);//zDegs += 180;
        else if (endPoint.x >= startPoint.x && endPoint.y >= startPoint.y) zDegs += 270;

        return zDegs;
    }

    public void Destroy(Vector2 pos)
    {
        //Debug.Log("Destroying Bullet");
        gameObject.transform.position = pos;
        //allowedToMove = false;
    }
}
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

        if (!canAttack) return;

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
        Vector2 toTarget = targetPoint - spawnPoint;

        GameObject go = Instantiate(spittle, spawnPoint, Quaternion.LookRotation(new Vector3(toTarget.x, 0f, 0f)));
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
}
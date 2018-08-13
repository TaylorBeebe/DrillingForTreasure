using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StickerEnemy : Enemy
{

    public GameObject headSticker;
    public float attackAnimationTime = 1.2f;

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
        Instantiate(headSticker, target.transform.position, Quaternion.identity);
        Destroy(gameObject);
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;

public class StickerEnemy : Enemy
{
    public GameObject headSticker;
    public float attackAnimationTime = 1.2f;
    bool wasHit = false;
    SpriteRenderer renderer;
    public Sprite deathSprite;
    SortingGroup group;

    public override void Start()
    {
        base.Start();
        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        group = GetComponent<SortingGroup>();

        target = enemyManager.Player;
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
        base.OnAttack();
        Instantiate(headSticker, target.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        renderer.sprite = deathSprite;
        group.enabled = false;

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
}


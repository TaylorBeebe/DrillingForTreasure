using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NestEnemy : Enemy
{

    public float minSpawnDelay = 5f;
    public float maxSpawnDelay = 8f;
    public GameObject creatureToSpawn;

    public Sprite deathSprite;
    public Sprite attackSprite;

    public float attackAnimationTime = 0.5f;

    bool wasHit = false;
    SpriteRenderer renderer;

    public override void Start()
    {
        base.Start();

        renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();

        aiAgent.canMove = false;
        QueueSpawn();
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

    void QueueSpawn()
    {
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        Invoke("SpawnCreature", delay);
    }

    void SpawnCreature()
    {
        Instantiate(creatureToSpawn, this.transform.position, Quaternion.identity);
        QueueSpawn();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnFollow()
    {
        base.OnFollow();
    }

    public override void OnAttack()
    {
        base.OnAttack();
    }

    public override void OnDeath()
    {
        base.OnDeath();

        renderer.sprite = deathSprite;
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


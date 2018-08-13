using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;

public class SpitterEnemy : Enemy
{

    public GameObject spittle;
    public float attackAnimationTime = 0.35f;
    bool wasHit = false;
    SpriteRenderer renderer;
    [SerializeField] float spitSpeed = 10f;
    [SerializeField] float spitDespawnTime = 4f;
    SortingGroup group;
    public Sprite deathSprite;
    public Sprite attackSprite;
    public Sprite aliveSprite;

    public override void Start()
    {
        base.Start();
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        group = GetComponent<SortingGroup>();
        target = enemyManager.Player;
    }
    public override void Update()
    {
        base.Update();
        aiAgent.canMove = canMove;
        //aiAgent.canMove = true;
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
        AttackAnimation();
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

    public override void OnDeath()
    {
        base.OnDeath();
        renderer.sprite = deathSprite;
        group.enabled = false;
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
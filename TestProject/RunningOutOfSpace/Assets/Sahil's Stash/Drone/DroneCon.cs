using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCon : MonoBehaviour
{
    public Transform owner;
    [Range(0, 10)]
    public float floatRadius = 10f;
    public float moveSpeed = 10f;
    public bool canAttack = true;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public float bulletDespawnTime = 4f;
    public int damage = 10; 
    public float timeBetweenAttacks = 2f; 



    Vector2 currentVel;

    public List<Transform> enemies = new List<Transform>();
    public Transform currentTarget;

    [Range(0, 200)]
    public float enemyCheckDist = 10f;

    public enum droneStates
    {
        follow,
        moveToTarget,
    }
    public droneStates dStates;
    // Use this for initialization
    void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player").transform;
        dStates = droneStates.follow;
    }

    // Update is called once per frame
    void Update()
    {
        switch (dStates)
        {
            case droneStates.follow:
                Follow(owner.transform.position, floatRadius);
                
                break;
            case droneStates.moveToTarget:
               // GetEnemies(enemyCheckDist);
                Follow(currentTarget.position, 0.1f);
                break;

        }

        if (canAttack) Attack(); 
    }

    void Attack()
    {
        //currentTarget = GetClosestEnemy(enemies.ToArray());
        currentTarget = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));

        canAttack = false;
        if (currentTarget == null)
        {
            // Shorter wait to check for new targets if didn't find any previously
            Invoke("UpdateCanAttack", 1f);
            return;
        }

        Vector2 spawnPoint = transform.position;
        float zDir = GetShootAngle(spawnPoint, currentTarget.transform.position);
        GameObject go = Instantiate(bullet, spawnPoint, Quaternion.Euler(0, 0, zDir)); 
        go.GetComponent<DroneBullet>().InheritValues(damage, bulletSpeed, bulletDespawnTime);

        Invoke("UpdateCanAttack", timeBetweenAttacks);
        canAttack = false; 
    }

    void UpdateCanAttack()
    {
        canAttack = true;
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

    void Follow(Vector2 target, float minFollowThreshold)
    {
        Vector2 dir2move = target - (Vector2)transform.position;
        Vector2 dir2moved = dir2move.normalized;
        Vector2 movepos = dir2moved * (dir2move.magnitude - minFollowThreshold);

        if (((Vector3)target - transform.position).magnitude > minFollowThreshold)
        {
            transform.position = Vector2.SmoothDamp(transform.position, target, ref currentVel, 0.7f);
        }
    }

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin.transform;
    }
}

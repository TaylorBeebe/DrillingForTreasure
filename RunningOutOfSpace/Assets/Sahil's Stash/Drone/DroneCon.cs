using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCon : MonoBehaviour
{
    public Transform owner;
    [Range(0, 10)]
    public float floatRadius = 10f;
    public float moveSpeed = 10f;

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
        dStates = droneStates.follow;
    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = GetClosestEnemy(enemies.ToArray());
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

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}

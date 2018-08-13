using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{

    int damage;
    float speed;
    float despawnTime;

    // Use this for initialization
    void Start()
    {
        //print("Despawn time is: " + despawnTime);
        Destroy(gameObject, despawnTime);
    }

    public void InheritValues(int _damage, float _speed, float _despawnTime)
    {
        //print("Inheriting!");
        damage = _damage;
        speed = _speed;
        despawnTime = _despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    void Hit() //TODO  Needs hooking up to a collider
    {
        //GameObject enemy = GameObject.FindGameObjectWithTag("Enemy"); //TODO  assign based on colliders
        //enemy.GetComponent<HealthAndVariables>().TakeDamage(damage); //TODO
        Destroy(gameObject);
    }
}
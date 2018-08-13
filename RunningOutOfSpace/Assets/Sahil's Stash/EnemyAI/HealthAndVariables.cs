using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndVariables : MonoBehaviour {
    [Range(0,10000)]
    public float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (this.gameObject.tag == "Player") {
            Debug.Log("TAG IS PLAYER ON HIT");
        }

        // player? enemy? drill? 

    }
}

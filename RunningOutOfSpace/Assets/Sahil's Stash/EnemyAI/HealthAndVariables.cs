using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndVariables : MonoBehaviour {
    [Range(0,1000)]
    public float health = 100f;

    /*
    public static void DoDamage(float damage, Transform instance)
    {
        HealthAndVariables script = instance.GetComponent<HealthAndVariables>();

        if (script.health - damage > 0 && script.health > 0)
        {
            script.health -= damage;
        }
        else
        {
            script.health = 0;
        }
    }
    */

    public void TakeDamage(float damage)
    {
        health -= damage;

        // player? enemy? drill? 

    }
}

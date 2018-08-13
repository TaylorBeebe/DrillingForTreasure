using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndVariables : MonoBehaviour {
    [Range(0,999999)]
    public float Health = 100f;

    public static void DoDamage(float damage,HealthAndVariables instance)
    {
        if (instance.Health - damage > 0 && instance.Health > 0)
        {
            instance.Health -= damage;
        }
        else
        {
            instance.Health = 0;
        }
    }
}

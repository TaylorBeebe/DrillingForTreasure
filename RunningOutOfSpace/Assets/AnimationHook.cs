using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AnimationHook : MonoBehaviour {
    public GameObject graphic;
    public Animator anim;
    public Enemy ai;
    public Vector3 baseScale;
    private void Start()
    {
        ai = GetComponent<Enemy>();
        graphic = transform.GetChild(0).gameObject;
        anim = graphic.GetComponent<Animator>();
        baseScale = graphic.transform.localScale;
    }
    private void Update()
    {
        Vector3 vel = ai.aiAgent.velocity;
        //anim.SetFloat("Velocity", vel.magnitude);
        if (ai.enemyStates == Enemy.EnemyStates.follow)
        {
            if (vel.x > 0)
            {
                graphic.transform.localScale = baseScale;
            }
            if (vel.x < 0)
            {
                graphic.transform.localScale = new Vector3(baseScale.x * -1,baseScale.y,baseScale.z);
            }
        }
    }
}

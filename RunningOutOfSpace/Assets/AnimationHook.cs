using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AnimationHook : MonoBehaviour {
    public GameObject graphic;
    public Animator anim;
    public Enemy ai;
    public Vector3 baseScale;
    public bool isLookingLeft;
    private void Start()
    {
        ai = GetComponent<Enemy>();
        graphic = transform.GetChild(0).gameObject;
        anim = graphic.GetComponent<Animator>();
        baseScale = graphic.transform.localScale;
        if (ai == GetComponent<NestEnemy>())
        {
            ai = GetComponent<NestEnemy>();
        }
    }

    //Type of Enemy
    public enum EType
    {
        Normal,
        Nest
    }
    public EType eType;
    private void Update()
    {
        switch (eType)
        {   
            //if The Enemy is normal type ie has 3 states Idle , Walk, Attack, Death
            //so Either the Enemy is Nest Type or normal type
            case EType.Normal:
                //gets the velocity of ai agent
                Vector3 vel = ai.aiAgent.velocity;

                //if the ai state is follow
                if (ai.enemyStates == Enemy.EnemyStates.follow)
                {
                    //sets the animator float "velocity" to velocity of ai
                    //we use velocity to determine if we want to use idle sprite of the walking sprite
                    anim.SetFloat("Velocity", vel.magnitude);

                    //if the agent is moving right
                    if (vel.x > 0)
                    {
                        //then turn him to right 
                        LookDir(false);
                    }
                    //else turn him left
                    if (vel.x < 0)
                    {
                        LookDir(true);
                    }
                }
                //if enemy is in attack state
                if (ai.enemyStates == Enemy.EnemyStates.attack)
                {
                    //set animator bool "isAttaking" 
                    anim.SetBool("isAttacking", true);
                    //We get the direction toward target
                    Vector2 TargetDir = ai.target.position - transform.position;

                    //if Target is on the left of enemy while attacking turn aI to left
                    if (TargetDir.x > 0)
                    {
                        LookDir(false);
                    }
                    else
                    {
                        LookDir(true);
                    }

                }
                break;
            //if The Enemy is Nest Type
            //I decided to use a diffrent animation controller for nest which wasn't necessary but never mind
            case EType.Nest:
                //determine if ai is a Nest
                if(ai == GetComponent<NestEnemy>())
                {
                    ai = GetComponent<NestEnemy>();
                    //if We are in State is Dead then we set animator bool to dead
                    if (GetComponent<NestEnemy>().enemyStates == Enemy.EnemyStates.dead)
                    {
                        anim.SetBool("isDead", true);
                    }
                    else
                    {
                        anim.SetBool("isDead", false);
                    }
                }
                break;
        }
    }

    //Turn the Sprite Left Or Right
    public void LookDir(bool Left)
    {
        if (!Left)
        {
            graphic.transform.localScale = baseScale;
        }
        if (Left)
        {
            graphic.transform.localScale = new Vector3(baseScale.x * -1, baseScale.y, baseScale.z);
        }
    }
}

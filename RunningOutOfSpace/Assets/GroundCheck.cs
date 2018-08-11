using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    CharacterController2D cc2d;

    void Start()
    {
        cc2d = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
    }

	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Ground")
            cc2d.SendMessage("TouchingGround", true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Ground")
            cc2d.SendMessage("TouchingGround", false);
    }
}

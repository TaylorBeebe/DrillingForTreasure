using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerOnHead : MonoBehaviour {

    int stickyPress = 0;
    public int shiftPressKillCount = 5;
    Transform player;
    bool canAttack = true;
    int damage = 1;
    float timeBetweenAttacks = 0.2f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update()
    {
        // If we nest as parent, we don't need to lock movement 
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - 0.01f, player.transform.position.z); 
        

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            stickyPress++;
        }

        if (stickyPress >= shiftPressKillCount)
        {
            Destroy(gameObject);
            // TODO could do with corpse
        }

        if (canAttack)
        {
            player.GetComponent<HealthAndVariables>().TakeDamage(damage);
            canAttack = false;
            Invoke("UpdateCanAttack", timeBetweenAttacks);
            print("BITE!");
        }
    }

    void UpdateCanAttack()
    {
        canAttack = true;
    }
}

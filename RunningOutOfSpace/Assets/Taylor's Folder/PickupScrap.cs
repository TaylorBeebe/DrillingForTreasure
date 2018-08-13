using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScrap : MonoBehaviour {

    void FixedUpdate() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {

            Debug.Log("Pickup");
            //add scrap to player
            other.gameObject.GetComponent<CharacterController2D>().scrap += 10;
            Destroy(gameObject, 0f);
        }
    }

}

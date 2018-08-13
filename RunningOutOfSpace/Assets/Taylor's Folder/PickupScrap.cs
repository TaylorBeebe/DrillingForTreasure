using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScrap : MonoBehaviour {



    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {

            Debug.Log("Pickup");
            //add scrap to player

            Destroy(gameObject, 0f);
        }
    }

}

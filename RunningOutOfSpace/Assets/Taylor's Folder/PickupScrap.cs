using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScrap : MonoBehaviour {

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    public float amplitude = 0.1f;
    public float frequency = 1.2f;

    void Start() {
        posOffset = transform.position;
    }

    void FixedUpdate() {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            
            other.gameObject.GetComponent<CharacterController2D>().scrap += 10;
            Destroy(gameObject, 0f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] [Range(100, 1000)] float bulletSpeed;

    Camera _camera;

    bool aloudToMove = false;
    Rigidbody2D rb2d;
    Vector2 bulletPos;

	// Use this for initialization
	void Start () {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        transform.position = GameObject.Find("Gun Tip").transform.position;
        transform.eulerAngles = GameObject.Find("Gun Tip").transform.eulerAngles;
        aloudToMove = true;
	}
	
	// Update is called once per frame
	void Update () {
        bulletPos = (Vector2)_camera.WorldToViewportPoint(transform.position);
        rb2d.AddRelativeForce(Vector2.right * bulletSpeed);
        if(bulletPos.x < 0 || bulletPos.x > 1 || bulletPos.y < 0 || bulletPos.y > 1)
        {
            Destroy(gameObject);
        }
	}
}

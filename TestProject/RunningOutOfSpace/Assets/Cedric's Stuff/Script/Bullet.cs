using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField] [Range(100, 1000)] float bulletSpeed;

    Camera _camera;
    CharacterController2D cc2d;

    bool facingRight;
    bool allowedToMove = false;
    Rigidbody2D rb2d;
    Vector2 bulletPos;
    SpriteRenderer sr;

    bool destroyed;

	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        cc2d = GameObject.Find("Player").GetComponent<CharacterController2D>();
        facingRight = cc2d.m_FacingRight;
        transform.position = GameObject.Find("Gun Tip").transform.position;
        if(cc2d.m_FacingRight)transform.eulerAngles = GameObject.Find("Gun Tip").transform.eulerAngles;
        if (!cc2d.m_FacingRight)transform.eulerAngles = -GameObject.Find("Gun Tip").transform.eulerAngles;
        if (!cc2d.m_FacingRight)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            sr.flipX = true;
        }
        allowedToMove = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (allowedToMove)
        {
            bulletPos = (Vector2)_camera.WorldToViewportPoint(transform.position);
            if (facingRight)
                rb2d.AddRelativeForce(Vector2.right * bulletSpeed);
            else
                rb2d.AddRelativeForce(Vector2.left * bulletSpeed);
            if (bulletPos.x < 0 || bulletPos.x > 1 || bulletPos.y < 0 || bulletPos.y > 1)
            {
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
	}

    public void Destroy(Vector2 pos) {
        //Debug.Log("Destroying Bullet");
        gameObject.transform.position = pos;
        allowedToMove = false;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}

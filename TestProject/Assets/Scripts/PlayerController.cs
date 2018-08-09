using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour {
    //Written by Cedric.

    [SerializeField] [Range(100,600)] float movementSpeed;
    [SerializeField] Sprite sUp;
    [SerializeField] Sprite sDown;
    [SerializeField] Sprite sLeft;
    [SerializeField] Sprite sRight;

    private bool canMove = true;

    private int horzAxis;
    private int vertAxis;

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    
    private enum dirToFace {Up, Down, Left, Right};
    private dirToFace dirFacing;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        horzAxis = (int)Input.GetAxisRaw("Horizontal");
        vertAxis = (int)Input.GetAxisRaw("Vertical");

        if (canMove)
        {
            if (horzAxis == 1)
            {
                dirFacing = dirToFace.Right;
                sr.sprite = sRight;
                rb2d.velocity = new Vector2(movementSpeed * horzAxis * Time.deltaTime, 0);
            }
            else if (horzAxis == -1)
            {
                dirFacing = dirToFace.Left;
                sr.sprite = sLeft;
                rb2d.velocity = new Vector2(movementSpeed * horzAxis * Time.deltaTime, 0);
            }
            else if (vertAxis == 1)
            {
                dirFacing = dirToFace.Up;
                sr.sprite = sUp;
                rb2d.velocity = new Vector2(0, movementSpeed * vertAxis * Time.deltaTime);
            }
            else if (vertAxis == -1)
            {
                dirFacing = dirToFace.Down;
                sr.sprite = sDown;
                rb2d.velocity = new Vector2(0, movementSpeed * vertAxis * Time.deltaTime);
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }

        if(Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}

    private void OnCollisionEnter2D(Collider col)
    {
        string tag = col.gameObject.transform.tag;
        if(tag == "Interactable")
        {
            if(Input.GetButtonDown("Interactable"))
            {
                InteractionManager im;
                im = col.GetComponent<InteractionManager>();
                im.OnInteract.Invoke();
                canMove = false;
            }
        }
    }

    public void CanMove()
    {
        canMove = true;
    }

    
}

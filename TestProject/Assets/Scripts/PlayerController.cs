using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour {
    //Written by Cedric.

    [SerializeField] [Range(100 ,600)] float movementSpeed;
    [SerializeField] Sprite sUp;
    [SerializeField] Sprite sDown;
    [SerializeField] Sprite sLeft;
    [SerializeField] Sprite sRight;

    private bool canMove = true;
    private bool nearLever = false;

    private int horzAxis;
    private int vertAxis;

    private SpriteRenderer sr;
    private Rigidbody2D rb2d;
    
    private enum dirToFace {Up, Down, Left, Right};
    private dirToFace dirFacing;

    private GameObject lever;

	void Start () {
        sr = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
	}

	void Update ()
    {
        horzAxis = (int)Input.GetAxisRaw("Horizontal");
        vertAxis = (int)Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(horzAxis, vertAxis);
        if (canMove)
        {
            rb2d.velocity = dir.normalized * movementSpeed * Time.deltaTime;

            if (horzAxis == 1)
            {
                dirFacing = dirToFace.Right;
                sr.sprite = sRight;
            }
            else if (horzAxis == -1)
            {
                dirFacing = dirToFace.Left;
                sr.sprite = sLeft;
            }
            else if (vertAxis == 1)
            {
                dirFacing = dirToFace.Up;
                sr.sprite = sUp;
            }
            else if (vertAxis == -1)
            {
                dirFacing = dirToFace.Down;
                sr.sprite = sDown;
            }
        }

        //Defaulted to I
        if(Input.GetButtonDown("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Defaulted to E
        if(Input.GetButtonDown("Interactable") && nearLever)
        {
            canMove = false;
            InteractionManager im;
            im = lever.GetComponent<InteractionManager>();
            im.OnInteract.Invoke();   
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string tag = col.gameObject.transform.tag;

        if(tag == "Interactable")
        {     
            nearLever = true;
            lever = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        string tag = col.gameObject.transform.tag;

        if (tag == "Interactable")
        {
            nearLever = false;
            lever = null;
        }
    }

    public void CanMove()
    {
        canMove = true;
    }
}

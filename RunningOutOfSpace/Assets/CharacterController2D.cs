using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {

    [Header("Editable")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] public float health = 100;
    [SerializeField] float cameraClampL;
    [SerializeField] float cameraClampR;
    [SerializeField] [Range(0.01f, 1)] float dampTime;
    [SerializeField] [Tooltip("If not true makes movement smoother")] bool rawAxis;

    [Header("Reference")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject deathOverlay;
    [SerializeField] GameObject bulletPrefab;

    private Vector3 _vel = Vector3.zero;
    private bool m_GroundCheck = true;
    private bool m_FacingRight = true;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        ValueCheck();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        MoveCamToPlayer();
        PlayerMovement();
        CheckDeath();
	}

    


    void MoveCamToPlayer()
    {
        Vector3 point = _camera.WorldToViewportPoint(transform.position); //Get's Player's Position in World to View Port;
        Vector3 delta = transform.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, point.y, point.z));  //Finds the difference between the middle of the screen/camera and our targets pos
        Vector3 destination = transform.position + delta; //Gets the correct position for the target to bein the center of the camera

        //print("Point: " + point + "Target Pos:" + target.position + "Delta: " + delta + "Destination" + destination);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _vel, dampTime);

        _camera.transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraClampL, cameraClampR), transform.position.y, transform.position.z);
    }

    void ValueCheck()
    {
        if (cameraClampL == cameraClampR) Debug.LogError("The Camera Clamp Values are the same on in Character Controller 2D on " + gameObject.name + ".");
        if (moveSpeed == 0) Debug.LogError("Move Speed not set in Character Controller 2D on " + gameObject.name + ".");
        if (jumpForce == 0) Debug.LogError("Jump Force not set in Character Controller 2D on " + gameObject.name + ".");
        if (_camera == null) Debug.LogError("The Camera is not referenced in CharacterController2D on " + gameObject.name + ".");
        if (deathOverlay == null) Debug.LogError("The Death Overlay is not referenced in CharacterController2D on " + gameObject.name + ".");
        if (EditorApplication.isPlaying && _camera == null)
        {
            EditorApplication.isPlaying = false;
            EditorUtility.DisplayDialog("Error", "Camera is not referenced in CharacterController2D on " + gameObject.name + ".", "Ok");
        }
    }

    void PlayerMovement()
    {
        float horzAxis;
        if (rawAxis)
            horzAxis = Input.GetAxisRaw("Horizontal");
        else
            horzAxis = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && m_GroundCheck)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
        }

        rb2d.velocity = new Vector2(moveSpeed * horzAxis * Time.deltaTime, rb2d.velocity.y);

        GetDirectionFacing(horzAxis);
        PlayerAttack();
    }

    void PlayerAttack()
    {
        if(Input.GetButtonDown("Attack"))
        {
            Instantiate(bulletPrefab);
        }
    }

    void CheckDeath()
    {
        if (health <= Mathf.Epsilon)
        {
            if (EditorApplication.isPlaying && deathOverlay == null)
            {
                EditorApplication.isPlaying = false;
                EditorUtility.DisplayDialog("Error", "Death Overlay is not referenced in CharacterController2D on " + gameObject.name + ".", "Ok");
            }
            Time.timeScale = 0;
            deathOverlay.SetActive(true);
        }
    }


    void GetDirectionFacing(float hAxis)
    {
        if (hAxis > 0)
        {
            m_FacingRight = true;
        }
        else if (hAxis < 0)
        {
            m_FacingRight = false;
        } else
        {
            return;
        }
    }

    void SetAnimation()
    {
        if(m_FacingRight)
        {
            sr.flipX = false;
        } else
        {
            sr.flipX = true;
        }
    }
    

    public void TouchingGround(bool tg)
    {
        m_GroundCheck = tg;
    }

    public void TakeDamage(float aod) //aod = AmountOfDamage
    {
        health -= aod;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour {

    [Header("Editable")]
    [SerializeField] [Range(100, 1000)] float movementSpeed;
    [SerializeField] [Range(0.0f, 1.0f)] float cameraDampTime;
    [SerializeField] public float health = 100;
    [SerializeField] [Tooltip("Might Give More Accurate Movement When True")] bool rawAxis;

    [Header("Firing")]
    [SerializeField] [Range(1,100)] int magazine;
    [SerializeField] [Range(0.0f, 1.0f)] [Tooltip("Time between each shot fired")] float fireRate;
    [SerializeField] [Range(0.0f, 2.0f)] float reloadTime;

    [Header("Camera")]
    [SerializeField] float cameraClampL;
    [SerializeField] float cameraClampR;
    [SerializeField] float cameraClampUp;
    [SerializeField] float cameraClampBot;

    [Header("Reference")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject deathOverlay;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator anim;
    private GameObject armPivot;
    private Vector3 _vel = Vector3.zero;
    private bool m_FacingRight = true;
    private bool _isDead = false;

	void Start () {
        ValueCheck();
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        armPivot = GameObject.Find("Arm Pivot");
	}
	
	void Update()
    {
        PlayerMovement();
        SetAnimation();
        CamMoveToPlayer();
        CheckDeath();
        GunSetRotation();
    }

    void PlayerMovement()
    {
        float horzAxis;
        float vertAxis;
        if(rawAxis)
        {
            horzAxis = Input.GetAxisRaw("Horizontal");
            vertAxis = Input.GetAxisRaw("Vertical");
        } else
        {
            horzAxis = Input.GetAxis("Horizontal");
            vertAxis = Input.GetAxis("Vertical");
        }

        rb2d.velocity = new Vector2(movementSpeed * horzAxis * Time.deltaTime, movementSpeed * vertAxis * Time.deltaTime);
        GetDirectionFacing(horzAxis);
    }

    void SetAnimation()
    {

        int horzAxis = (int)Input.GetAxisRaw("Horizontal") ;
        if(m_FacingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        } else if(!m_FacingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
        }
        
        if(horzAxis != 0)
        {
            anim.SetInteger("State", 1);
        } else
        {
            anim.SetInteger("State", 0);
        }
    }

    void CamMoveToPlayer()
    {
        Vector3 point = _camera.WorldToViewportPoint(transform.position); //Get's Player's Position in World to View Port;
        Vector3 delta = transform.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, point.y, point.z));  //Finds the difference between the middle of the screen/camera and our targets pos
        Vector3 destination = _camera.transform.position + delta; //Gets the correct position for the target to bein the center of the camera

        //print("Point: " + point + "Target Pos:" + target.position + "Delta: " + delta + "Destination" + destination);

        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, destination, ref _vel, cameraDampTime);

        _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, cameraClampL, cameraClampR), _camera.transform.position.y, _camera.transform.position.z);
    }

    void ValueCheck()
    {
        if(health == 0 || _camera == null || movementSpeed == 0)
        {
            if (EditorApplication.isPlaying && deathOverlay == null)
            {
                EditorApplication.isPlaying = false;
                if (_camera == null) EditorUtility.DisplayDialog("Error", "Camera is not referenced in CharacterController2D on '" + gameObject.name + "'.", "Ok");
                if (movementSpeed == 0) EditorUtility.DisplayDialog("Error", "Movement Speed is not set in CharacterController2D on '" + gameObject.name + "'.", "Ok");
                if (health == 0) EditorUtility.DisplayDialog("Error", "Health is not set in CharacterController2D on '" + gameObject.name + "'.", "Ok");
            }
        }
    }

    void CheckDeath()
    {
        if (health <= Mathf.Epsilon)
        {
            if (EditorApplication.isPlaying && deathOverlay == null)
            {
                EditorApplication.isPlaying = false;
                EditorUtility.DisplayDialog("Error", "Death Overlay is not referenced in CharacterController2D on '" + gameObject.name + "'.", "Ok");
            }
            Time.timeScale = 0;
            deathOverlay.SetActive(true);
        }
    }

    void GetDirectionFacing(float hAxis)
    {
        if(hAxis > 0)
        {
            m_FacingRight = true;
        } else if(hAxis < 0)
        {
            m_FacingRight = false;
        } else
        {
            return;
        }
    }

    void GunSetRotation()
    {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = _camera.WorldToViewportPoint(armPivot.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        Mathf.Clamp(angle, 155, -155);
        armPivot.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Mathf.Clamp(angle, -25, 25)));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    

    public void TakeDamage(float aod) //aod = AmountOfDamage
    {
        health -= aod;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    [SerializeField] [Range(0.0f, 1.0f)] [Tooltip("Time between each shot fired")] float fireRate;
    [SerializeField] float charge;
    [SerializeField] [Range(0.0f, 1.0f)] float chargeLoseRate;
    [SerializeField] [Range(0.0f, 1.0f)] float rechargeRate;

    [Header("Camera")]
    [SerializeField] float cameraClampL;
    [SerializeField] float cameraClampR;
    [SerializeField] float cameraClampUp;
    [SerializeField] float cameraClampBot;

    [Header("UI Stuff")]
    [SerializeField] Image playerHealthUI;
    [SerializeField] Image gunChargeUI;

    [Header("Reference")]
    [SerializeField] Camera _camera;
    [SerializeField] GameObject deathOverlay;
    [SerializeField] GameObject bulletPrefab;

    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator anim;
    private GameObject armPivot;
    private Vector3 _vel = Vector3.zero;
    private HealthAndVariables hav;
    private float maxCharge;
    private float maxHealth;
    private int mag;
    [HideInInspector] public bool m_FacingRight = true;
    private bool _isDead = false;
    private bool _canShoot = true;

	void Start () {
        ValueCheck();
        maxCharge = charge;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        armPivot = GameObject.Find("Arm Pivot");
        hav = gameObject.GetComponent<HealthAndVariables>();
        maxHealth = hav.health;
    }
	
	void Update()
    {
        health = hav.health;
        PlayerShoot();
        SetAnimation();
        CamMoveToPlayer();
        CheckDeath();
        GunSetRotation();
        UIUpdate();
    }
    
    void FixedUpdate()
    {
        PlayerMovement();
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
        
    }

    void PlayerShoot()
    {
        //print(charge);
        if(Input.GetMouseButton(0) && charge > 0 && _canShoot)
        {
            if (charge > 0 && _canShoot)
            {
                charge = charge - (maxCharge * chargeLoseRate);
                StartCoroutine(ShootFix());
                Instantiate(bulletPrefab);
            }
        } else if(charge <= Mathf.Epsilon)
        {
            if (charge < 0) charge = 0;   
        }
        if (!Input.GetMouseButton(0) && charge < maxCharge)
            charge = charge + (maxCharge * rechargeRate);
        else if (charge > maxCharge)
            charge = maxCharge;
    }

    void UIUpdate()
    {
        float uiInfo = charge / maxCharge;
        uiInfo *= 100;
        Mathf.Round(uiInfo);
        uiInfo /= 100;
        gunChargeUI.fillAmount = uiInfo;

        uiInfo = health / maxHealth;
        uiInfo *= 100;
        Mathf.Round(uiInfo);
        uiInfo /= 100;
        playerHealthUI.fillAmount = uiInfo;
    }



    void SetAnimation()
    {

        int horzAxis = (int)Input.GetAxisRaw("Horizontal");
        int vertAxis = (int)Input.GetAxisRaw("Vertical");
        if (m_FacingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        } else if(!m_FacingRight)
        {
            transform.eulerAngles = new Vector3(0, 180, transform.eulerAngles.z);
        }
        
        if(horzAxis != 0 || vertAxis != 0)
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
        Vector3 delta = transform.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));  //Finds the difference between the middle of the screen/camera and our targets pos
        Vector3 destination = _camera.transform.position + delta; //Gets the correct position for the target to bein the center of the camera

        //print("Point: " + point + "Target Pos:" + target.position + "Delta: " + delta + "Destination" + destination);

        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, destination, ref _vel, cameraDampTime);

        _camera.transform.position = new Vector3(Mathf.Clamp(_camera.transform.position.x, cameraClampL, cameraClampR), 
            Mathf.Clamp(_camera.transform.position.y, cameraClampBot, cameraClampUp), _camera.transform.position.z);
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

    //Todo fix if facing left, just update the angle calculation.
    void GunSetRotation()
    {
        float angle;

        //Get the Screen positions of the object
        Vector2 positionOnScreen = _camera.WorldToViewportPoint(armPivot.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)_camera.ScreenToViewportPoint(Input.mousePosition);

        /*//Get the angle between the points
        if(m_FacingRight)
            angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        else
            angle = -AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);

        print(angle);   

        //Ta Daaa
        if(m_FacingRight)armPivot.transform.rotation = Quaternion.Euler(new Vector3(armPivot.transform.rotation.x, 0, Mathf.Clamp(angle, -25,25)));
        if (!m_FacingRight) armPivot.transform.rotation = Quaternion.Euler(new Vector3(armPivot.transform.rotation.x, 180, Mathf.Clamp(angle, -25, 25)));
        */


        angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        print(angle);
        if (angle > -90 && angle < 90)
        {
            m_FacingRight = true;
        } else
        {
            m_FacingRight = false;
        }

        if (m_FacingRight)
            angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        else
            angle = -AngleBetweenTwoPoints(mouseOnScreen, positionOnScreen);

        if (m_FacingRight) armPivot.transform.rotation = Quaternion.Euler(new Vector3(armPivot.transform.rotation.x, 0, Mathf.Clamp(angle, -90, 90)));
        if (!m_FacingRight) armPivot.transform.rotation = Quaternion.Euler(new Vector3(armPivot.transform.rotation.x, 180, Mathf.Clamp(angle, -90, 90)));
        
    }

    IEnumerator ShootFix()
    {
        _canShoot = false;
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}

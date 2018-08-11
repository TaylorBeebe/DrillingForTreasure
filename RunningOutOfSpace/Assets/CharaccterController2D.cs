using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaccterController2D : MonoBehaviour {

    [Header("Editable")]
    [SerializeField] float moveSpeed;
    [SerializeField] float JumpForce;
    [SerializeField] [Range(0.01f, 1)] float dampTime;

    [Header("Reference")]
    [SerializeField] Camera camera;

    private Vector3 _vel = Vector3.zero;

    // Use this for initialization
    void Start () {
		if(camera == null)
        {
            Debug.LogError("Camera is not referenced in CharacterController2D.");
        }
	}
	
	// Update is called once per frame
	void Update () {
        MoveCamToPlayer();
	}


    void MoveCamToPlayer()
    {
        Vector3 point = camera.WorldToViewportPoint(transform.position); //Get's Player's Position in World to View Port;
        Vector3 delta = transform.position - camera.ViewportToWorldPoint(new Vector3(0.5f, point.y, point.z));  //Finds the difference between the middle of the screen/camera and our targets pos
        Vector3 destination = transform.position + delta; //Gets the correct position for the target to bein the center of the camera

        //print("Point: " + point + "Target Pos:" + target.position + "Delta: " + delta + "Destination" + destination);

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _vel, dampTime);

        camera.transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, 100), transform.position.y, transform.position.z);
    }
}

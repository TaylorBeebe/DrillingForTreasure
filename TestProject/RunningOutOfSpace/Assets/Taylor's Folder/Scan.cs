using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Scan : MonoBehaviour
{



    AstarPath path;
    // Use this for initialization
    void Start()
    {

        path = GetComponent<AstarPath>();

    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        Debug.Log("Scanning");
        path.Scan();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{

    // Use this for initialization
    //private Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    int layerMask = 1 << 8;
    private Collider[] Ground;
    public bool grounded;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        Ground = Physics.OverlapSphere(transform.position, groundCheckRadius, layerMask);
        if (Ground.Length != 0)
        {
            if (Ground[0].tag == "Plataforma")
            {
                grounded = true;

            }
            else
                grounded = false;

        }
        else
            grounded = false;
    }
    public bool getGrounded()
    {
        return grounded;
    }

}

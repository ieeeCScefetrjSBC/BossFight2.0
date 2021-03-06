﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    bool Touched;
    float Counting = 0f;
    public float TimeToFall;
    float DownSpeed = 0f;

    void Start()
    {
        Touched = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            Touched = true;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Touched = true;
            Destroy(gameObject, 10);
        }
    }*/

    void Update()
    {
        if (Touched == true)
        {
            Counting += Time.deltaTime;
        }
        if (Counting >= TimeToFall && Touched == true)
        {
            DownSpeed += Time.deltaTime / 5;
        }
        //Debug.Log(Touched);
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - DownSpeed, transform.position.z);
    }

    public bool getTouchedBool()
    {
        return this.Touched;
    }
}

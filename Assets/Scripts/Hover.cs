﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour {

 public float altitude;
 public float verticalSpeed;
 public float amplitude;
 
 private Vector3 tempPosition;

	// Use this for initialization
	void Start () {
		tempPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed)* amplitude;
		transform.position = new Vector3(transform.position.x,tempPosition.y + altitude,transform.position.z);
	}
}

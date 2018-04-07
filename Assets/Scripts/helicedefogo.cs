using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helicedefogo : MonoBehaviour {
    public Transform helice;
    public float vel;
	void Start ()
    {
        
	}
	
	void Update ()
    {
        helice.transform.Rotate(Vector3.up * Time.deltaTime * vel, Space.World);
	}
}

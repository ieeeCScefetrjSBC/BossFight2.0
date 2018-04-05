using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sopro : MonoBehaviour {

    private float Timer=0f;
    private GameObject Player;
    private GameObject Boss;
    private Vector3 direction;

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Boss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	void Update ()
    {
        direction = Boss.transform.position - Player.transform.position;
        direction = direction.normalized;
	
        Timer += Time.deltaTime;
		
	}
	
	void FixedUpdate
	{
     
		if (Timer > 5f && Timer < 10f)
            Player.GetComponent<Rigidbody>().AddForce(direction * 30);     
        
		
	}
}

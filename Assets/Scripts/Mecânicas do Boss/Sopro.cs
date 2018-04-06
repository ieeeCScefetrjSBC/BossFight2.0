using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sopro : MonoBehaviour {

    private float Timer=0f; //Timer placeholder
    private GameObject Player; //Objeto Player
    private GameObject Boss;  //Objeto Boss
    private Vector3 direction; //vetor direçao do player ao boss

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //Encontra o objeto Player
        Boss = GameObject.FindGameObjectWithTag("Boss"); //Encontra o objeto Boss
	}
	
	void Update ()
    {
        direction = Boss.transform.position - Player.transform.position; //Traça o vetor
        direction = direction.normalized; //Normaliza o vetor
	
        Timer += Time.deltaTime; //Inicia o timer
		
	}
	
	void FixedUpdate()
	{
     
		if (Timer > 5f && Timer < 10f) //Se o timer estiver entre 5 e 10 segundos:
            Player.GetComponent<Rigidbody>().AddForce(direction * 30); //Adciona uma força de 30 na direçaodo boss    
        
		
	}
}

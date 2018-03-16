﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao_Tiro_Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>()); //Ignora colisão com o player
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Boss")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Boss>().danoBoss(1);
            Destroy(this.gameObject);
        }
        else
        if (collision.gameObject.name.Equals("TiroPlayer(Clone)"))
        {

        }else
        {
            Destroy(this.gameObject);
        }
    }
}

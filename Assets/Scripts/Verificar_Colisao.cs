using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider>()); //Ignora colisão com o Boss
	}
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {
       
        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(1);
            Destroy(this.gameObject);
        }
        else
        if (collision.gameObject.tag.Equals("Plataforma"))
        { //Se for plataforma ele destroi a plataforma
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

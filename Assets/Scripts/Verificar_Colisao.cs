using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider>()); //Ignora colisão com o Boss
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara1").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara1
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara2").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara2
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara3").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara3
    }
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {
       
        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(1);
            Debug.Log("Atingiu");
            Destroy(this.gameObject);
        }
        else
        if (collision.gameObject.tag.Equals("Plataforma"))
        { //Se for plataforma ele destroi a plataforma
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

	}
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira dano
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(1);
            Destroy(this.gameObject);
        } else 
        if (collision.gameObject.tag.Equals("Plataforma")){ //Se for plataforma ele destroi a plataforma
            Destroy(collision.gameObject);
        } else
        if (collision.gameObject.tag.Equals("Chao")) //Se for chao ele se auto destroi
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

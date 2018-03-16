using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao_Tiro_Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {
        if (collision.gameObject.tag.Equals("Boss")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Boss>().danoBoss(1);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao : MonoBehaviour {

    // Use this for initialization
    private GameObject Boss;
	void Start () {
		if( GameObject.FindGameObjectWithTag("Boss")!= null)
			Boss = GameObject.FindGameObjectWithTag("Boss");
        	Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Boss").GetComponent<BoxCollider>()); //Ignora colisão com o Boss
		if( GameObject.FindGameObjectWithTag("Mascara1")!= null)
			Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara1").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara1
		if( GameObject.FindGameObjectWithTag("Mascara2")!= null)
			Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara2").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara2
		if( GameObject.FindGameObjectWithTag("Mascara3")!= null)
			Physics.IgnoreCollision(this.gameObject.GetComponent<SphereCollider>(), GameObject.FindGameObjectWithTag("Mascara3").GetComponent<BoxCollider>()); //Ignora colisão com a Mascara3
        
    }
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {

        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(100);// Aplica o dano 
            Debug.Log("Atingiu");
            Destroy(this.gameObject);
            float fireRate = Boss.GetComponent<Tiro_Boss>().getfireRate();// Adquire o valor atual de fireRate
            if(fireRate-0.15f>=0.4f) // Caso a redução leve a um valor igual ou superior ao fireRate mínimo
                Boss.GetComponent<Tiro_Boss>().setfireRate(fireRate-0.15f);// Diminui o Fire Rate
            else
                Boss.GetComponent<Tiro_Boss>().setfireRate(0.4f);// Caso seja inferior, reduz apenas ao valor mínimo
        }
        else
        {
            float Add= Boss.GetComponent<Tiro_Boss>().getfireRate(); // Adquire o valor do fireRate
            Boss.GetComponent<Tiro_Boss>().setfireRate(Add + 0.05f); // Incrementa em 0.05  
            Destroy(this.gameObject);
            
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

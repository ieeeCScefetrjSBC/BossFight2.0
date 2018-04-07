using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sopro : MonoBehaviour {

    private float Timer=0f; // Timer placeholder
    private GameObject Player; // Objeto do jogador na cena
    private GameObject Boss; // Objeto do boss na cena
    private Vector3 direction; // Vetor direção da força

	void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        Boss = GameObject.FindGameObjectWithTag("Boss"); // Encontra o Boss via tag
	}
	
	void Update ()
    {
        direction = Boss.transform.position - Player.transform.position; //Define o ponto inicial como a posição do jogador e o final como a posição do boss
        direction = direction.normalized; //normaliza o vetor
	
        Timer += Time.deltaTime; // inicia o timer
		
	}
	
	void FixedUpdate()
	{
     
		if (Timer > 5f && Timer < 10f) // Enquanto o timer estiver entre 5 e 10 segundos
            Player.GetComponent<Rigidbody>().AddForce(direction * 20); // Aplica força de 30 no vetor direction
        
		
	}
}

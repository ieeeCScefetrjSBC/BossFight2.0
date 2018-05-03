using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Tiro : MonoBehaviour {

    // Use this for initialization
    private GameObject Player; // Objeto do jogador
    private Vector3 Direction;// Direção para o jogador
    private Vector3 Wrong_Direction;// Direção errada do tiro
    private Vector3 Total_Force;
    private Rigidbody RB;// Rigidbody do Tiro
    
    private float Velocity, Timer=2f;// Velocidade do Tiro e Tempo de atualização da direção
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");// Define GameObject Player
        RB= GetComponent<Rigidbody>();// Define o Rigidbody RB
	}
	
	// Update is called once per frame
	void Update () {
        Total_Force = Total_Force * 0f;
        if(Timer>=0)// Caso ainda esteja atualizando:
            Direction = (Player.transform.position - this.transform.position).normalized; // Atualiza a direção para o jogador       
        if ((RB.velocity.normalized - Direction).magnitude >= 0.1f) // Se houver algum desvio no tiro
        {
            Wrong_Direction = RB.velocity.normalized; // Adquire a direção do desvio
            Velocity = 1.5f;
        }
        else       // se não houver desvio
        {
            Wrong_Direction = Wrong_Direction * 0f; // Zera a direção do desvio
            Velocity = 0.5f;
        }
        Total_Force += Direction * Velocity;
        Total_Force -= Wrong_Direction * Velocity/1.2f;
        RB.AddForce(Total_Force, ForceMode.VelocityChange);// Soma uma força em direção ao jogador com uma magnitude de "Velocity"
        Timer -= Time.deltaTime;// Decai o temporizador
	}
}

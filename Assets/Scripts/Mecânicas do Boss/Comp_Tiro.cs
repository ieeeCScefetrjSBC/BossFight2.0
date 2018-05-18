using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Tiro : MonoBehaviour {

    // Use this for initialization
    private GameObject Boss;// Objeto do Boss
    private GameObject Player; // Objeto do jogador
    private Vector3 Direction;// Direção para o jogador
    private Vector3 Wrong_Direction;// Direção errada do tiro
    private Vector3 Origin;// Onde se origina o tiro
    private Vector3 Total_Force;
    private Rigidbody RB;// Rigidbody do Tiro
    
    private float Velocity=0.5f, Timer=5f;// Velocidade do Tiro e Tempo de atualização da direção
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");// Define GameObject Player
        RB= GetComponent<Rigidbody>();// Define o Rigidbody RB
        Boss = GameObject.FindGameObjectWithTag("Boss");// Define GameObject Boss
        Origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Total_Force = Total_Force * 0f;// Reseta a força que será aplicada neste frame
        if(Timer>=0 && (Player.transform.position-Origin).magnitude > (this.transform.position-Origin).magnitude)// Caso ainda esteja atualizando:
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
        Timer -= Time.deltaTime;// Decai o temporizador
	}
    private void FixedUpdate()
    {
        Total_Force += Direction * Velocity;//  Soma uma força em direção ao jogador com uma magnitude de "Velocity"
        Total_Force -= Wrong_Direction * Velocity / 1.2f;// Soma uma força na direção oposta à direção "errada"
        RB.AddForce(Total_Force, ForceMode.VelocityChange);// Aplica a força total ao objeto
    }
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {

        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(100);// Aplica o dano 
            Debug.Log("Atingiu");
            Destroy(this.gameObject);
            float fireRate = Boss.GetComponent<Tiro_Boss>().getfireRate();// Adquire o valor atual de fireRate
            if (fireRate - 0.15f >= 0.4f) // Caso a redução leve a um valor igual ou superior ao fireRate mínimo
                Boss.GetComponent<Tiro_Boss>().setfireRate(fireRate - 0.15f);// Diminui o Fire Rate
            else
                Boss.GetComponent<Tiro_Boss>().setfireRate(0.4f);// Caso seja inferior, reduz apenas ao valor mínimo
        }
        else
        {
            if (Boss != null)
            {
                float Add;
                Tiro_Boss Tiro = Boss.GetComponent<Tiro_Boss>();
                Add = Tiro.getfireRate(); // Adquire o valor do fireRate
                Tiro.GetComponent<Tiro_Boss>().setfireRate(Add + 0.05f); // Incrementa em 0.05
            }
            Destroy(this.gameObject);

        }
    }
}

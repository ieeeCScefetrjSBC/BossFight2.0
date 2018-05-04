using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sopro : MonoBehaviour {

    private float TimerGround=0f; // Timer que o player não sai do chão
    private float TimerForce = 0f; // Tempo que a força é aplicada
    private bool RequestForce = false;
    private GameObject Player; // Objeto do jogador na cena
    private GameObject Boss; // Objeto do boss na cena
    private GameObject Grounder; //Objeto do chão
    private Vector3 direction; // Vetor direção da força
    private float ForceMultiplier = 0.1f; // Multiplicador da força
    private float DuracaoSopro = 2f;
    private float TempoAteSopro = 8f;

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        Boss = GameObject.FindGameObjectWithTag("Boss"); // Encontra o Boss via tag
        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder
    }

    void Update()
    {
        Debug.Log("TimerGround: " + TimerGround + ", TimerForce: " + TimerForce);

        direction = Boss.transform.position - Player.transform.position; //Define o ponto inicial como a posição do jogador e o final como a posição do boss
        direction = direction.normalized; //normaliza o vetor

        if (Player.GetComponent<CharacterController>().isGrounded) //Enquanto o player não sai do chão, inicia o timer 1
            TimerGround += Time.deltaTime;
        else
            TimerGround = 0f;

        if (TimerGround > TempoAteSopro && TimerGround < TempoAteSopro + DuracaoSopro)
        {
            RequestForce = true;
            TimerForce += Time.deltaTime;
        }

        if (TimerGround >= TempoAteSopro + DuracaoSopro)
        {
            RequestForce = false;
            TimerForce = 0f;
            if(TimerGround >= 10)
                TimerGround = 0f;
        }

        if (RequestForce) // Enquanto o timer estiver entre 5 e 10 segundos
        {
            Debug.Log("Carai ta voando");
            Player.GetComponent<CharacterController>().Move(direction*ForceMultiplier);
            //Player.GetComponent<Mov>().setExtra_Z(-ForceMultiplier);
        }
    }

}

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
    private float ForceMultiplier = 50; // Multiplicador da força
    private float DuracaoSopro = 2f;
    private float TempoAteSopro = 8f;
    private bool SoproAtivado = false; // Luciano esteve aqui muahahaha

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        Boss = GameObject.FindGameObjectWithTag("Boss"); // Encontra o Boss via tag
        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder
    }

    void Update()
    {

        direction = Boss.transform.position - Player.transform.position; //Define o ponto inicial como a posição do jogador e o final como a posição do boss
        direction = direction.normalized; //normaliza o vetor
        Debug.Log(direction);
        if (Player.GetComponent<Mov>().Grounded) //Enquanto o player não sai do chão, inicia o timer 1
            TimerGround += Time.deltaTime;
        else
        {
            if (!SoproAtivado)
                TimerGround = 0f;
        }

        Debug.Log(TimerGround);
            
        if (TimerGround > TempoAteSopro && TimerGround < TempoAteSopro + DuracaoSopro)
        {
            Debug.Log("Ativou request force");
            RequestForce = true;
            TimerForce += Time.deltaTime;
            SoproAtivado = true;
        }

        if (TimerGround >= TempoAteSopro + DuracaoSopro)
        {
            RequestForce = false;
            TimerForce = 0f;
            if(TimerGround >= 10)
                TimerGround = 0f;
            SoproAtivado = false;
        }

    }

    private void FixedUpdate()
    {
        if (RequestForce) // Enquanto o timer estiver entre 5 e 10 segundos
        {

            Player.GetComponent<Rigidbody>().AddForce(direction.normalized * ForceMultiplier, ForceMode.Force);
            Debug.Log("sopro");
        }
    }

}

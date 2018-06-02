using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sopro : MonoBehaviour {

    private float TimerGround=0f; // Timer que o player não sai do chão
    private float TimerAir = 0f; //Timer que o Player está no ar
    private float TimerForce = 0f; // Tempo que a força é aplicada
    private bool RequestForce = false;

    private GameObject Player; // Objeto do jogador na cena
    private GameObject Boss; // Objeto do boss na cena
    private GameObject Grounder; //Objeto do chão
    private Vector3 direction; // Vetor direção da força

    private float ForceMultiplier = 50; // Multiplicador da força
    private float DuracaoSopro = 5f;
    private float TempoAteSopro = 8f;
    private bool SoproAtivado = false; // Luciano esteve aqui muahahaha

    private int Pattern_Sopro = 0;
    private Comp_Call Comp_Call;// Script referente ao Comp_Call

    private GameObject Tell_Push;
    private GameObject Tell_Pull;
    private bool Tell_On = false; 

    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        Boss = GameObject.FindGameObjectWithTag("Boss"); // Encontra o Boss via tag
        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder

        Comp_Call=this.gameObject.GetComponent<Comp_Call>();

        Tell_Push = GameObject.FindGameObjectWithTag("Push");
        Tell_Pull = GameObject.FindGameObjectWithTag("Pull");

        Tell_Pull.SetActive(false);
        Tell_Push.SetActive(false);


    }

    void Update()
    {

        direction = Boss.transform.position - Player.transform.position; //Define o ponto inicial como a posição do jogador e o final como a posição do boss
        direction = direction.normalized; //normaliza o vetor
        if (Player.GetComponent<Mov>().Grounded || TimerAir < 0.5f) //Enquanto o player não sai do chão, inicia o timer 1
        {
            TimerGround += Time.deltaTime;
            TimerAir = 0;
        }       
        else
        {
            TimerAir += Time.deltaTime;
            Debug.Log(TimerAir);

            if (!SoproAtivado)
                TimerGround = 0f;
            else
                TimerGround += Time.deltaTime;
        }
  
        if (TimerGround > TempoAteSopro && TimerGround < TempoAteSopro + DuracaoSopro)
        {
            //Debug.Log("Ativou request force");
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

            Tell_On = false;
            Tell_Pull.SetActive(false);
            Tell_Push.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        if (RequestForce) // Enquanto o timer estiver entre 5 e 10 segundos
        {
            switch (Pattern_Sopro)
            {
                case 1:
                    Player.GetComponent<Rigidbody>().AddForce(direction.normalized * ForceMultiplier, ForceMode.Force);
                    if (!Tell_On)
                    {
                        Tell_Pull.SetActive(true);
                        Tell_On = true;
                    }
                    break;
                case 2:
                    Player.GetComponent<Rigidbody>().AddForce(-direction.normalized * ForceMultiplier, ForceMode.Force);
                    if (!Tell_On)
                    {
                        Tell_Push.SetActive(true);
                        Tell_On = true;
                    }
                    break;
            }
        }
    }

    public int Call (int Comando)
    {
        Pattern_Sopro = Comando; //Define qual será o padrao do sopro
        Comp_Call.setTempo(10f); //Define o tempo até a próxima mecanica

        return Pattern_Sopro;
    }

}

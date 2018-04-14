using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private bool Jumping = false; // Está pulando
    private bool JumpRequest = false; // Identifica que o player deve pular
    private bool Colidiu = false;//Determina se o player colidiu com a plataforma de pulo
    private float JumpForce = 0.6f; // Velocidade do pulo
    private float FallForce = -0.01f;// Velocidade de queda
    private float FallMultiplier = 1.1f;// Multiplicador da gravidade, deve ser sempre maior que 1
    private float Fator = 0f;//X da equação
    private float MultiplicadorPulo = 2.6f;//Multiplicador do pulo na plataforma de pulo
    private GameObject Grounder; //Objeto do chão

    void Start()
    {

        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder
    }

    void Update()
    {
        if(Colidiu){//se ele colidiu ele faz o pulo da plataforma de pulo
            if(Grounder.GetComponent<Grounded>().getGrounded()){
                if (!Jumping){
                    GetComponent<Mov>().setExtra_Y(MultiplicadorPulo*JumpForce);
                    Jumping = true;
                    JumpRequest = true;
                    FallForce = -0.001f;//diminui a força da queda do normal porque ele tava acelerando demais na queda
                    FallMultiplier = 1.01f;//idem pro multiplicador de queda
                }
            }
            Colidiu = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounder.GetComponent<Grounded>().getGrounded()) //Torna JumpRequest true quando a barra de espaço for pressionada e o player estiver no chão
            JumpRequest = true;

        if (JumpRequest)
        {
            if (!Jumping) //Caso ele ja não esteja pulando, aplica a força JumpForce
            {
                GetComponent<Mov>().setExtra_Y(JumpForce);
                Jumping = true;
            }
            else
            {

                if (GetComponent<Rigidbody>().velocity.y <= 0)
                {//Caso ele esteja caindo, aumenta a gravidade
                    Fator += Time.deltaTime;
                    FallForce += Physics.gravity.y * 0.002f * Mathf.Sqrt(Fator);
                }

                GetComponent<Mov>().setExtra_Y(FallForce);

                if (Grounder.GetComponent<Grounded>().getGrounded() && FallForce <= -0.02f) //Para ele quando estiver extremamente proximo ao chão
                {
                    JumpRequest = false;
                    Jumping = false;
                    Fator = 0f;
                    GetComponent<Mov>().setExtra_Y(-GetComponent<Mov>().getExtra_Y()); //Aplica uma força igual e reserva no eixo Y, negando o movimento
                    FallForce = -0.01f;
                }
            }
        }

        FallForce = -0.01f;
        FallMultiplier = 1.1f;
    }
    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "PlataformaPulo"){
            Colidiu = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    private bool Jumping = false; // Está pulando
    private bool JumpRequest = false; // Identifica que o player deve pular
    private float JumpForce = 0.48f; // Velocidade do pulo
    private float FallForce = -0.015f;// Velocidade de queda
    private float FallMultiplier = 1.1f;// Multiplicador da gravidade, deve ser sempre maior que 1
    private float Fator = 0f;//X da equação
    private GameObject Grounder; //Objeto do chão

    void Start()
    {

        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder
    }

    void Update()
    {

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
    }
}

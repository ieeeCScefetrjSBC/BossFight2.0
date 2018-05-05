using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour {

    private float TimerGround = 0f; // Timer que o player não sai do chão
    private GameObject Player; // Objeto do jogador na cena
    private GameObject Grounder; //Objeto do chão
    private float TempoAteInverterControles = 10f;
    private bool InverterControlesAtivado = false;

    // Use this for initialization
    void Start () {

        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        Grounder = GameObject.FindWithTag("Grounder"); //Identifica o objeto Grounder

    }
	
	// Update is called once per frame
	void Update () {
        if (Player.GetComponent<CharacterController>().isGrounded) //Enquanto o player não sai do chão, inicia o timer 1
            TimerGround += Time.deltaTime;
        else
        {
            if (!InverterControlesAtivado)
                TimerGround = 0f;
        }
        

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Plataforma(S) (3)" || hit.gameObject.name == "Plataforma(O) (3)" || hit.gameObject.name == "Plataforma(L) (3)" || hit.gameObject.name == "Plataforma(N) (3)" || hit.gameObject.name == "Plataforma(NL)" || hit.gameObject.name == "Plataforma(SO)" || hit.gameObject.name == "Plataforma(SL)" || hit.gameObject.name == "Plataforma(NO)")
        {
            Debug.Log("a");
        }
    }
}

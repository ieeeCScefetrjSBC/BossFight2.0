using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour {

    private float TimerGround = 0f; // Timer que o player não sai do chão
    private GameObject Player; // Objeto do jogador na cena
    private GameObject Grounder; //Objeto do chão
    private float TempoAteInverterControles = 10f;
    private bool InverterControlesAtivado = false;
    private bool Colidiu = false;
    private MovimentoPlayer MovimentoPlayer;

    // Use this for initialization
    void Start () {

        Player = GameObject.FindGameObjectWithTag("Player"); // Encontra o player via tag
        MovimentoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimentoPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Colidiu) //Enquanto o player não sai do chão, inicia o timer 1
            TimerGround += Time.deltaTime;
        else
        {
            TimerGround = 0f;
        }
        if(TimerGround >= 1f && TimerGround <= 15f)
        {
            MovimentoPlayer.SetInverterControlesAtivado(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "Plataforma(S) (3)" || hit.gameObject.name == "Plataforma(O) (3)" || hit.gameObject.name == "Plataforma(L) (3)" || hit.gameObject.name == "Plataforma(N) (3)" || hit.gameObject.name == "Ponte(NL)" || hit.gameObject.name == "Ponte(SO)" || hit.gameObject.name == "Ponte(SL)" || hit.gameObject.name == "Ponte(NO)")
        {
            Colidiu = true;
            Debug.Log("Colidiu atrás");
        }
        else
        {
            Colidiu = false;
        }
    }
}

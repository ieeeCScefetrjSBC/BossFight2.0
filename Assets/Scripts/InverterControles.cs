using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour {

    private float TimerInvert = 0f; // Timer que o player não sai do chão
    private float TempoAteInverterControles = 10f;//Tempo até o começo da inversão dos controles
    private float TempoDuracaoInversao = 5f;//Tempo de duração da inversão dos controles
    //private bool InverterControlesAtivado = false;
    private bool Colidiu = false;
    private MovimentoPlayer MovimentoPlayer;

    // Use this for initialization
    void Start () {
        MovimentoPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimentoPlayer>();//Vai pegar o script MovimentoPlayer
    }
	
	// Update is called once per frame
	void Update () {
        if (Colidiu)//se o player colidiu com uma plataforma externa e ainda não colidiu com alguma outra coisa
        {
            TimerInvert += Time.deltaTime;//começa a contar o tempo
            Debug.Log(TimerInvert);
        }
        else//se colidiu com alguma outra plataforma
        {
            TimerInvert = 0f;//zera o tempo
        }
        if(TimerInvert >= TempoAteInverterControles && TimerInvert <= TempoAteInverterControles + TempoDuracaoInversao)//se o timer chegar até o tempo designado e o player ainda estiver la por tras
        {
            MovimentoPlayer.SetInverterControlesAtivado(true);//o atributo InverterControlesAtivado, pertencente ao script MovimentoPlayer, vai ser setado como true
        }
        if(TimerInvert > TempoDuracaoInversao + TempoAteInverterControles || !Colidiu)//se o tempo de duração passou
        {
            TimerInvert = 0f;
            MovimentoPlayer.SetInverterControlesAtivado(false);//o atributo InverterControlesAtivado, pertencente ao script MovimentoPlayer, vai ser setado como false
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//checa se o player colidiu com alguma das plataformas de trás
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

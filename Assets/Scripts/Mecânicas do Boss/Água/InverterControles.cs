using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour {

    private float TimerInvert = 0f; // Timer que o player não sai do chão
    private float TempoAteInverterControles = 10f;//Tempo até o começo da inversão dos controles
    private float TempoDuracaoInversao = 5f;//Tempo de duração da inversão dos controles
    private bool InverterControlesAtivado = false;
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
        }
        else//se colidiu com alguma outra plataforma
        {
            TimerInvert = 0f;//zera o tempo
            MovimentoPlayer.SetInverterControlesAtivado(false);

        }
        if(TimerInvert >= TempoAteInverterControles)//se o timer chegar até o tempo designado e o player ainda estiver la por tras
        {
            MovimentoPlayer.SetInverterControlesAtivado(true);//o atributo InverterControlesAtivado, pertencente ao script MovimentoPlayer, vai ser setado como true
            InverterControlesAtivado=true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//checa se o player colidiu com alguma das plataformas de trás
    {
        if (hit.gameObject.name == "plataforma S (4)" || hit.gameObject.name == "plataforma O (4)" || hit.gameObject.name == "plataforma L (4)" || hit.gameObject.name == "plataforma N (4)" || hit.gameObject.name == "ponte NL" || hit.gameObject.name == "ponte SO" || hit.gameObject.name == "ponte SL" || hit.gameObject.name == "ponte NO")
        {
            Colidiu = true;

        }
        else
        {
            Colidiu = false;
        }
    }

    public bool getInverterControlesAtivado(){
    	return this.InverterControlesAtivado;
    }

    public void setInverterControlesAtivado(bool inverter){
    	this.InverterControlesAtivado = inverter;
    }

    public void setTimerInvert(float timer){
    	this.TimerInvert = timer;
    }
}

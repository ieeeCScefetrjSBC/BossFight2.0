using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour
{

    private MoveRigidbody moveScript;

    private float timeLeftUntilInversion = 0f;     // Timer que o player não sai do chão
    private float totalTimeUntilInversion = 10f;    //Tempo até o começo da inversão dos controles
    
    private bool invertedControlOn = false;
    private bool collided = false;

    // Use this for initialization
    void Start()
    {
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveRigidbody>();//Vai pegar o script MovimentoPlayer
    }

    // Update is called once per frame
    void Update()
    {
        if (collided)//se o player colidiu com uma plataforma externa e ainda não colidiu com alguma outra coisa
        {
            timeLeftUntilInversion += Time.deltaTime;//começa a contar o tempo
        }
        else//se colidiu com alguma outra plataforma
        {
            timeLeftUntilInversion = 0f;//zera o tempo
            moveScript.SetInvertedControl(false);

        }
        if (timeLeftUntilInversion >= totalTimeUntilInversion)//se o timer chegar até o tempo designado e o player ainda estiver la por tras
        {
            moveScript.SetInvertedControl(true);//o atributo InverterControlesAtivado, pertencente ao script MovimentoPlayer, vai ser setado como true
            invertedControlOn = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "plataforma N (4)" || collision.gameObject.name == "plataforma S (4)"
         || collision.gameObject.name == "plataforma O (4)" || collision.gameObject.name == "plataforma L (4)"
         || collision.gameObject.name == "ponte NO" || collision.gameObject.name == "ponte NL"
         || collision.gameObject.name == "ponte SO" || collision.gameObject.name == "ponte SL")
        {
            collided = true;
        }
        else
            collided = false;
    }

    public bool getInverterControlesAtivado()
    {
        return this.invertedControlOn;
    }

    public void setInverterControlesAtivado(bool inverter)
    {
        this.invertedControlOn = inverter;
    }

    public void setTimerInvert(float timer)
    {
        this.timerForInversion = timer;
    }
}
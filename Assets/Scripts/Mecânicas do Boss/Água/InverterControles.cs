

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverterControles : MonoBehaviour
{

    private MoveRigidbody moveScript;

    private float timeLeftUntilInversion = 0f;      // Timer faltando para inverter
    private float totalTimeUntilInversion = 10f;    // Tempo total até inversão

    private bool invertedControlOn = false;
    private bool timerOn = false;

    // Use this for initialization
    void Start()
    {
        moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MoveRigidbody>();//Vai pegar o script MovimentoPlayer
        timeLeftUntilInversion = totalTimeUntilInversion;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn)                     // se o player colidiu com uma plataforma externa e ainda não colidiu com alguma outra coisa
        {
            timeLeftUntilInversion -= Time.deltaTime;           // começa a contar o tempo

            Debug.Log("Tempo até inversão: " + timeLeftUntilInversion);
        }

        if (timeLeftUntilInversion <= 0f)           // se o timer chegar até o tempo designado e o player ainda estiver la por tras
        {
            timerOn = false;
            invertedControlOn = true;

            Debug.Log("INVERSÃO ATIVADA");
        }

        moveScript.SetInvertedControl(invertedControlOn);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string objectName = collision.gameObject.name;

        if (objectName == "plataforma N (4)" || objectName == "plataforma S (4)"
         || objectName == "plataforma O (4)" || objectName == "plataforma L (4)"
         || objectName == "ponte NO"         || objectName == "ponte NL"
         || objectName == "ponte SO"         || objectName == "ponte SL"
         || objectName == "Parede 1"         || objectName == "Parede 2"
         || objectName == "Parede 3"         || objectName == "Parede 4")
        {
            if (!timerOn)
            {
                timeLeftUntilInversion = totalTimeUntilInversion;
                timerOn = true;

                Debug.Log("CONTAGEM PARA INVERSÃO INICIADA");
            }
        }
        else
        {
            timerOn = false;
            invertedControlOn = false;

            Debug.Log("INVERSÃO DESATIVADA");
        }
    }

    public bool getInverterControlesAtivado()
    {
        return this.invertedControlOn;
    }

    public void setInverterControlesAtivado(bool invert)
    {
        this.invertedControlOn = invert;
    }

    public void setTimerInvert(float time)
    {
        this.timeLeftUntilInversion = time;
    }
}
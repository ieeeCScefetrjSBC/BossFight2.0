using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Antidoto : MonoBehaviour {

    private bool Colidir = false;
    private GameObject PlayerOBJ;// Objeto do jogador na cena
    private InverterControles inverter;
    private GameObject Boss;// Objeto do Boss
    private Comp_Bomba Comp_Bomba;

    // Use this for initialization
    void Start() {
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player");// Encontra o objeto via tag
        inverter = GameObject.FindGameObjectWithTag("Player").GetComponent<InverterControles>();
        
        Boss= GameObject.FindGameObjectWithTag("Boss");//DEfine o Boss
        Comp_Bomba= Boss.GetComponent<Comp_Bomba>();
    }

    // Update is called once per frame
    void Update() {

        if(inverter.getInverterControlesAtivado()){
            Comp_Bomba.Call(4);
            Debug.Log("ABC");
            //Comp_Bomba.setCiclo(1.5f);
            Comp_Bomba.setContador(0);
            if(Colidir){
                inverter.setInverterControlesAtivado(false);
                inverter.setTimerInvert(0f);
            }
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "bomba_antidoto" || collision.gameObject.name == "bomba_antidoto (1)")
        {
            Colidir = true;
            Debug.Log("Colidiu");
        }
        else
        {
            Colidir = false;
        }
    }
}

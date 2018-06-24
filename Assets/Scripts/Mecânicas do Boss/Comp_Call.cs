using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Call : MonoBehaviour {

    // Use this for initialization
    private float Tempo = 5f; // Tempo para chamar uma mecânica
    private Comp_Bomba Comp_Bomba;// Script referente ao Comp_Bomba
    private Tiro_Boss Tiro_Boss;// Script referente ao Tiro_Boss
	private Comp_Helice Comp_Helice; // Script referente à Comp_Helice
    private Sopro Comp_Sopro; // Script referente ao sopro
    private Mascara_Script mascara_Script;

    private delegate void myMechanics(int Pattern);//
    private myMechanics[] Mechanics= new myMechanics[3];// Variável que guarda métodos
    private int Contador;// Indica a posição da mecânica a ser ativada e qual padrão será usado
    public int[] Index_Mechanics;// Posição da mecânica no Array Mechanics
    public int[] Module;// Padrão da mecânica(Caso tenha apenas um padrão, colocar 0)

    void Start () {
        Comp_Bomba = this.gameObject.GetComponent<Comp_Bomba>();// Define quem é Comp_Bomba
        Tiro_Boss = this.gameObject.GetComponent<Tiro_Boss>();// Define quem é Tiro_Boss
		Comp_Helice = this.gameObject.GetComponent<Comp_Helice>(); // Define quem é Comp_Helice
        Comp_Sopro = this.gameObject.GetComponent<Sopro>(); // Define que é Cómp_Sopro
        mascara_Script = this.gameObject.GetComponent<Mascara_Script>(); // Define o que é mascara_Script

        Mechanics[0] = Call_Bomba;// Espaço 0 é a mecânica de bomba!
        Mechanics[1] = Call_Helice; // Espaço 1 é a mecânica de helice!
        Mechanics[2] = Call_Sopro; // Espaço 2 é a mecânica de sopro!
    }
	
	// Update is called once per frame
	void Update () {

        Tempo -= Time.deltaTime; // Descendo o contador
        Debug.Log(Tempo);
        if(Tempo<=0) // Ativou a mecânica
        {
            if (Contador + 1 <= Index_Mechanics.Length)
            {
                Mechanics[Index_Mechanics[Contador]](Module[Contador]);// Passa o padrão para a mecânica
                Contador += 1;
                switch(Contador)
                {
                    case 5:
                        mascara_Script.ChooseMask(3);
                        Tiro_Boss.setTiro("Tiro Agua");
                        Tiro_Boss.setSpray_Tiro("Spray_Tiro Agua");
                        break;
                    case 7:
                        mascara_Script.ChooseMask(2);
                        Tiro_Boss.setTiro("Tiro Raio");
                        Tiro_Boss.setSpray_Tiro("Spray_Tiro Raio");
                        break;
                    case 12:
                        mascara_Script.ChooseMask(1);
                        Tiro_Boss.setTiro("Tiro");
                        Tiro_Boss.setSpray_Tiro("Spray_Tiro");
                        break;
                }
            }
            else
            {
                Contador = 0;// Próxima mecânica e padrão
                //Debug.Log("Condição else");
            }

         Debug.Log(Contador);
        }
        if(Tiro_Boss.getfireRate()>=1f)// Caso o rate de tiro seja maior ou igual a 1
        {
            Tiro_Boss.setPattern("Sprayed_Shots");// Ativa o Tiro em Spray
            
        }
	}
    private void Call_Bomba(int Pattern) // Define o padrão de bomba
    {
        Comp_Bomba.Call(Pattern);// Passa o padrão para a mecânica Bomba
    }
    private void Call_Helice(int Pattern) // Define o padrão de helice
    {
        Comp_Helice.Call(Pattern); // Passa o padrão de sopro
    }
    private void Call_Sopro(int Pattern) // Define o padrão de sopro
    {
        Comp_Sopro.Call(Pattern); // Passa o padrão de sopro
    }
    public void setTempo(float Tempo)// Modifica o tempo para a próxima mecânica
    {
        this.Tempo = Tempo;// Novo tempo 
    }
   
}

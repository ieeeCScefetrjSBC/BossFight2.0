using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Call : MonoBehaviour {

    // Use this for initialization
    private float Tempo = 5f; // Tempo para chamar uma mecânica
    private Comp_Bomba Comp_Bomba;// Script referente ao Comp_Bomba
    private Tiro_Boss Tiro_Boss;// Script referente ao Tiro_Boss
	private Comp_Helice Comp_Helice; // Script referente à Comp_Helice
    private delegate void myMechanics(int Pattern);//
    private myMechanics[] Mechanics= new myMechanics[1];// Variável que guarda métodos
    private int Contador;// Indica a posição da mecânica a ser ativada e qual padrão será usado
    public int[] Index_Mechanics;// Posição da mecânica no Array Mechanics
    public int[] Module;// Padrão da mecânica(Caso tenha apenas um padrão, colocar 0)
    void Start () {
        Comp_Bomba = this.gameObject.GetComponent<Comp_Bomba>();// Define quem é Comp_Bomba
        Tiro_Boss = this.gameObject.GetComponent<Tiro_Boss>();// Define quem é Tiro_Boss
		Comp_Helice = this.gameObject.GetComponent<Comp_Helice>(); // Define quem é Comp_Helice
        Mechanics[0] = Call_Bomba;// Espaço 0 é a mecânica de bomba!
        Mechanics[1] = Call_Helice; // Espaço 1 é a mecânica de helice!
    }
	
	// Update is called once per frame
	void Update () {
        Tempo -= Time.deltaTime; // Descendo o contador
        if(Tempo<=0) // Ativou a mecânica
        {
            Mechanics[Index_Mechanics[Contador]](Module[Contador]);// Passa o padrão para a mecânica
            if(Contador+1 <= Index_Mechanics.Length)
            Contador += 1;// Próxima mecânica e padrão
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
        Comp_Helice.Call(Pattern);
    }
    public void setTempo(float Tempo)// Modifica o tempo para a próxima mecânica
    {
        this.Tempo = Tempo;// Novo tempo 
    }
   
}

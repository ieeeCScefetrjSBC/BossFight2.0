using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Call : MonoBehaviour {

    // Use this for initialization
    private float Tempo = 5f; // Tempo para chamar uma mecânica
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Tempo -= Time.deltaTime; // Descendo o contador
        if(Tempo<=0) // Ativou a mecânica
        {
            Call_Bomba(2);
        }
        if(GetComponent<Tiro_Boss>().getfireRate()>=1f)
        {
            GetComponent<Tiro_Boss>().setPattern("Sprayed_Shots");
            
        }
	}
    private void Call_Bomba(int Pattern)
    {
        this.gameObject.GetComponent<Comp_Bomba>().Call(2);// Passa o padrão 2 para a mecânica Bomba
        Tempo = 25f;// Tempo para a próxima execução
    }
   
}

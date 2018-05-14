using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Bomba : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Bombas_Fogo;// Objetos das Bombas_Fogo
    public GameObject[] Bombas_Raio;// Objetos das Bombas_Raio
    public GameObject[] Bombas_Antidoto;// Objetos das Bombas_Antídoto
    private int i=0;// Contador
    private float Ciclo = 5f;// Ciclo de ativação
    private int Pattern_Bomba = 0;// Padrão de aparecimento, sendo 0= nada acontece;
    private Comp_Call Comp_Call;// Script referente ao Comp_Call

    void Start () {
        Comp_Call=this.gameObject.GetComponent<Comp_Call>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (Pattern_Bomba)
        {
            case 1:
                Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
                if (Ciclo <= 0)// Caso acabe o contador
                {
                    Ciclo = 5f; // Recomeça a contar
                    Bombas_Fogo[i].SetActive(true); // Ativa a bomba
                    i++; // Passa para a próxima bomba
                }
                if (i == 4) // Acabaram as bombas
                {
                    Pattern_Bomba = 0; // Acabou o padrão
                    i = 0; // Reseta o contador
                }
                break;
            case 2:
                Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
                if (Ciclo <= 0) // Caso acabe o contador
                {
                    Debug.Log(i);
                    Ciclo = 5f;// Recomeça a contar
                    Bombas_Fogo[i + 3].SetActive(true); // Ativa a bomba
                    i--;// Passa para a próxima

                }
                if (i == -4)// Acabaram as bombas
                {
                    Pattern_Bomba = 0;// Acabou o padrão
                    i = 0;// Reseta o contador
                }
                break;
            case 3:
                Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
                if (Ciclo <= 0)// Caso acabe o contador
                {
                    Ciclo = 5f; // Recomeça a contar
                    Bombas_Raio[i].SetActive(true); // Ativa a bomba
                    i++; // Passa para a próxima bomba
                }
                if (i == 4) // Acabaram as bombas
                {
                    Pattern_Bomba = 0; // Acabou o padrão
                    i = 0; // Reseta o contador
                }
                break;
            case 4:
                Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
                if (Ciclo <= 0)// Caso acabe o contador
                {
                    Ciclo = 2f; // Recomeça a contar
                    Bombas_Antidoto[i].SetActive(true); // Ativa a bomba
                    i++; // Passa para a próxima bomba
                }
                if (i == 2) // Acabaram as bombas
                {
                    Pattern_Bomba = 0; // Acabou o padrão
                    i = 0; // Reseta o contador
                }
                break;
        }
    }
    public int Call(int Comando)
    {
        Pattern_Bomba = Comando;// Define qual será o padrão de bomba
        if(Pattern_Bomba!=4)
            Comp_Call.setTempo(25f);// Tempo para as Bombas Fogo e Raio
        else
            Comp_Call.setTempo(10f);// Tempo para a Bomba Antídoto
        return Pattern_Bomba;
    }
    public void setCiclo(float Ciclo)// Modifica o valor do Ciclo(tempo entre spawn de bomba)
    {
        this.Ciclo= Ciclo;

    }
    public void setContador(int i)// Quantidade de bombas que vão ser instanciadas
    {
        this.i= i;
    }
}

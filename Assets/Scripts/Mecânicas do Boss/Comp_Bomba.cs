﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Bomba : MonoBehaviour {

    // Use this for initialization
    public GameObject[] Bombas_Fogo;
    public GameObject[] Bombas_Raio;
    private int i=0;// Contador
    private float Ciclo = 5f;// Ciclo de ativação
    private int Pattern_Bomba = 0;// Padrão de aparecimento, sendo 0= nada acontece;

    void Start () {
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
        }
    }
    public int Call(int Comando)
    {
        Pattern_Bomba = Comando;
        return Pattern_Bomba;
    }
}

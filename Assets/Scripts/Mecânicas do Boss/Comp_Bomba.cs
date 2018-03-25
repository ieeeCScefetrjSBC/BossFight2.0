using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Bomba : MonoBehaviour {

    // Use this for initialization
    public GameObject Bomba_1;// Bomba SO
    public GameObject Bomba_2;// Bombaa NO
    public GameObject Bomba_3;// Bomba NL
    public GameObject Bomba_4;// Bomba SL
    private float Ciclo = 20f;// Ciclo de ativação
    private int Pattern_Bomba = 0;// Padrão de aparecimento, sendo 0= nada acontece;

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Pattern_Bomba==1)// Caso o padrão chamado seja 1
        {
            Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
            if (Ciclo <= 15f && Ciclo >= 10f)// Impede de ser reativado após 5 secs existindo
                Bomba_1.SetActive(true);// Ativa a bomba SO
            if (Ciclo <= 10f && Ciclo >= 5f)// Impede de ser reativado após 5 secs existindo
                Bomba_2.SetActive(true);// Ativa a bomba NO
            if (Ciclo <= 5f && Ciclo >= 0f)// Impede de ser reativado após 5 secs existindo
                Bomba_3.SetActive(true);// Ativa a bomba NL
            if (Ciclo <= 0f)// Fim do Ciclo
            {
                Bomba_4.SetActive(true);// Ativa a bomba SL
                Pattern_Bomba = 0;// Fim do Padrão
            }
        }
        if (Pattern_Bomba == 2)// Caso o padrão chamado seja 1
        {
            Ciclo -= Time.deltaTime;// Ciclo diminuindo contador
            if (Ciclo <= 15f && Ciclo >= 10f)// Impede de ser reativado após 5 secs existindo
                Bomba_4.SetActive(true);// Ativa a bomba SO
            if (Ciclo <= 10f && Ciclo >= 5f)// Impede de ser reativado após 5 secs existindo
                Bomba_3.SetActive(true);// Ativa a bomba NO
            if (Ciclo <= 5f && Ciclo >= 0f)// Impede de ser reativado após 5 secs existindo
                Bomba_2.SetActive(true);// Ativa a bomba NL
            if (Ciclo <= 0f)// Fim do Ciclo
            {
                Bomba_1.SetActive(true);// Ativa a bomba SL
                Pattern_Bomba = 0;// Fim do Padrão
            }
        }

    }
    public int Call(int Comando)
    {
        Pattern_Bomba = Comando;
        return Pattern_Bomba;
    }
}

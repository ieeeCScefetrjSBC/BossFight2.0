using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Helice : MonoBehaviour {
	
	public GameObject Helice_Fogo; 		// Prefab da helice de fogo
	public GameObject Helice_Tempestade;	    // Prefab da helice de tempestade
	public GameObject Helice_Gelo;			    // Prefab da helice de Água
    private GameObject[] helice;                // Variável para acessar as helices instanciadas

	public Vector3[] HelicesFogo_offset;	    // Número de helices e posição de instanciamento em relação ao boss	FOGO
    public Vector3[] HelicesGelo_offset;	    // Número de helices e posição de instanciamento em relação ao boss	GELO

    [SerializeField] private float Timer_ActivateFogo;            //Timer para ativar script das helices de fogo;
    [SerializeField] private float Timer_ActivateGelo;            //Timer para ativar script das helices de gelo;
    [SerializeField] private float Tempo_Fogo;      //Tempo de duração da helice de fogo
    [SerializeField] private float Tempo_Tempst;    //Tempo de duração da helice de tempestade
    [SerializeField] private float Tempo_Gelo;      //Tempo de duração da helice de água
	private int Pattern_Helice = 0 ;  // Padrão de aparecimento, sendo 0 = nada acontece
    private bool Instanciou_Padrão1 = false;
    private bool Instanciou_Padrão2 = false;
    private bool AtivouScriptFogo = false;
    private bool AtivouScriptGelo = false;

    private Comp_Call Comp_Call;

	void Start () {
        helice = new GameObject[HelicesFogo_offset.Length];
        Comp_Call = this.GetComponent<Comp_Call>();
	}

    void Update()
    {
        switch (Pattern_Helice)
        {
            case 0:
                Instanciou_Padrão1 = false;
                Instanciou_Padrão2 = false;
                AtivouScriptFogo = false;
                AtivouScriptGelo = false;
                break;
            case 1:     // Padrão para máscara de FOGO!!!!

                if (!Instanciou_Padrão1)
                {
                    for (int i = 0; i < HelicesFogo_offset.Length; i++)
                    {
                        helice[i] = (GameObject)Instantiate(Helice_Fogo, this.gameObject.transform.position + HelicesFogo_offset[i], Quaternion.identity);
                    }
                }
                if (Timer_ActivateFogo >= 0)
                    Timer_ActivateFogo -= Time.deltaTime;
                else if(Timer_ActivateFogo < 0 && !AtivouScriptFogo)
                {
                    for (int i = 0; i<HelicesFogo_offset.Length; i++)
                    {
                        helice[i].GetComponent<HeliceDeFogo>().enabled = true;
                    }
                    AtivouScriptFogo = true;
                }
                Instanciou_Padrão1 = true;
                break;
            case 2:
                if (!Instanciou_Padrão2)
                {
                    for (int i = 0; i < HelicesGelo_offset.Length; i++)
                    {
                        helice[i] = (GameObject)Instantiate(Helice_Gelo, this.gameObject.transform.position + HelicesGelo_offset[i], Quaternion.identity);
                    }
                }
                if ( Timer_ActivateGelo>= 0)
                     Timer_ActivateGelo -= Time.deltaTime;
                else if(Timer_ActivateGelo < 0 && !AtivouScriptGelo)
                {
                    for (int i = 0; i < HelicesGelo_offset.Length; i++)
                    {
                        helice[i].GetComponent<HeliceDeGelo>().enabled = true;
                    }
                    AtivouScriptGelo = true;
                }
                Instanciou_Padrão2 = true;
                break;
        }
    }

    public int Call(int Comando)
    {
        Pattern_Helice = Comando;
        if (Pattern_Helice == 1)
        {
            Comp_Call.setTempo(Tempo_Fogo);
            //Debug.Log("Helice de fogo chamada");
        }
        else if (Pattern_Helice == 2)
        {
            Comp_Call.setTempo(Tempo_Gelo);
            //Debug.Log("Helice de gelo chamada");
        }
        else
            Comp_Call.setTempo(0);
        return Pattern_Helice;
    }
    
    /*public void setBool_padrao1(bool Booleana)
    {
        this.Instanciou_Padrão1 = Booleana;
    }
    public void setBool_Padrao2(bool Booleana)
    {
        this.Instanciou_Padrão2 = Booleana;
    }*/
}

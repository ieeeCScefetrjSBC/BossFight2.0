using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Helice : MonoBehaviour {
	
	public GameObject Helice_Fogo; 		// Prefab da helice de fogo
	public GameObject Helice_Tempestade;	    // Prefab da helice de tempestade
	public GameObject Helice_Agua;			    // Prefab da helice de Água
    private GameObject[] helice;                // Variável para acessar as helices instanciadas

	public Vector3[] HelicesFogo_offset;	    // Número de helices e posição de instanciamento em relação ao boss	

    [SerializeField] private float Timer_ActivateFogo;            //Timer para ativar script das helices;
    [SerializeField] private float Tempo_Fogo;      //Tempo de duração da helice de fogo
    [SerializeField] private float Tempo_Tempst;    //Tempo de duração da helice de tempestade
    [SerializeField] private float Tempo_Agua;      //Tempo de duração da helice de água
	private int Pattern_Helice = 0 ;  // Padrão de aparecimento, sendo 0 = nada acontece

    private Comp_Call Comp_Call;

	void Start () {
        helice = new GameObject[HelicesFogo_offset.Length];
        Comp_Call = this.GetComponent<Comp_Call>();
	}

    void Update()
    {
        
        switch (Pattern_Helice)
        {
            case 1:     // Padrão para máscara de FOGO!!!!
                for (int i = 0; i<HelicesFogo_offset.Length; i++)
                {
                   
                    helice[i] = (GameObject)Instantiate(Helice_Fogo, this.gameObject.transform.position + HelicesFogo_offset[i], Quaternion.identity, this.gameObject.transform);
                    helice[i].GetComponent<HeliceDeFogo>().enabled = true;
                }
                if (Timer_ActivateFogo >= 0)
                    Timer_ActivateFogo -= Time.deltaTime;
                else
                {
                    for(int i=0; helice[i] != null; i++)
                    {
                        helice[i].GetComponent<HeliceDeFogo>().enabled = true;
                    }
                }
                break;

        }
    }

    public int Call(int Comando)
    {
        Pattern_Helice = Comando;
        if(Pattern_Helice == 1)
        {
            Comp_Call.setTempo(Tempo_Fogo);
        }
        return Pattern_Helice;
    }
}

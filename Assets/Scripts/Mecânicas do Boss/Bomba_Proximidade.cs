using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Proximidade : MonoBehaviour {

    // Use this for initialization
    public Transform Jogador; // Adquire o valor da posição do jogador em X Y e Z
    private float D= 5f; // Valor absoluto utilizado para formar a distância de ativação
    private float Zona_Dano = 15f;// Valor absoluto utilizado para formar a distância de dano
    private bool Triggered=false;// Booleana para indicar a ativação da bomba relógio
    private float Tempo=10f;// Tempo para a detonação do ataque
    private int Dano=3;// Dano que será causado pela habilidade
    private float Duration = 6f;// Duração do objeto na cena
    private GameObject PlayerOBJ;// Objeto do jogador na cena
    public Light Luz;// Objeto gerador de luz na bomba
	void Start () {
       PlayerOBJ = GameObject.FindGameObjectWithTag("Player");// Encontra o objeto via tag
        Luz.enabled= false;
    }
	
	// Update is called once per frame
	void Update () {
        if ((Mathf.Abs(transform.position.x - Jogador.transform.position.x) <= D)&&(Mathf.Abs(transform.position.y-Jogador.transform.position.y)<=D)&&(Mathf.Abs(transform.position.z-Jogador.transform.position.z)<=D)){// Verifica se o jogador está a uma distância D do objeto
            Triggered = true;// Ativa o ataque
            Luz.enabled= true;

        }
        if (Triggered) // Caso esteja ativado, desce a contagem
        {
            Tempo -= Time.deltaTime;//Contagem descendo a cada frame
            if (Tempo<= 1f)
            {
                Luz.areaSize = new Vector2(200f, 200f);
                Luz.intensity = 100f;
            }
            if(Tempo<=0f)// Caso tenha acabado o tempo
            {
                Debug.Log("KABOOM");
                Triggered = false;// Impede sucessivas explosões
                               
                if ((Mathf.Abs(transform.position.x - Jogador.transform.position.x) <= Zona_Dano) && (Mathf.Abs(transform.position.y - Jogador.transform.position.y) <= Zona_Dano) && (Mathf.Abs(transform.position.z - Jogador.transform.position.z) <= Zona_Dano)) {// Verifica se o jogador está na zona de dano
                    PlayerOBJ.GetComponent<Vida_Player>().danoPlayer(Dano);//Aplica dano ao jogador
                    Duration = 6f;
                    
                }
                this.gameObject.SetActive(false);
            }
            
        }
        else
        {
            Duration -= Time.deltaTime;
            if (Duration<= 1f)
            {
                Luz.areaSize = new Vector2(15f, 15f);
                Luz.intensity = 50f;
            }
            if (Duration <= 0f)
            {
                Duration = 6f;
                this.gameObject.SetActive(false);
               
            }
                
        }
		
	}
}

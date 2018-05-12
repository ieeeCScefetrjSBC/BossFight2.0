using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Raio : MonoBehaviour {

    // Use this for initialization
    public Transform Jogador; // Adquire o valor da posição do jogador em X Y e Z
    private float D = 5f; // Valor absoluto utilizado para formar a distância de ativação
    private float Zona_Dano = 15f;// Valor absoluto utilizado para formar a distância de dano
    private bool Triggered = true;// Booleana para indicar a ativação da bomba relógio
    private float Tempo = 10f;// Tempo para a detonação do ataque
    private int Dano = 3;// Dano que será causado pela habilidade
    private float Duration = 6f;// Duração do objeto na cena
    private GameObject PlayerOBJ;// Objeto do jogador na cena
    private GameObject Helice_Vento;// Objeto da Helice de Vento
    public Light Luz;// Objeto gerador de luz na bomba
    public GameObject Explosion;
    public AudioSource ExplosionSound;

    void Start()
    {
        Helice_Vento = GameObject.FindGameObjectWithTag("Shuriken_Vento");
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player");// Encontra o objeto via tag
        Luz.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(transform.position.x - Jogador.transform.position.x) <= D) && (Mathf.Abs(transform.position.y - Jogador.transform.position.y) <= D) && (Mathf.Abs(transform.position.z - Jogador.transform.position.z) <= D))
        {// Verifica se o jogador está a uma distância D do objeto
            Triggered = false;// Ativa o ataque
            Luz.enabled = false;

        }
        if (Triggered) // Caso esteja ativado, desce a contagem
        {

            Tempo -= Time.deltaTime;//Contagem descendo a cada frame
            if (Tempo <= 1f)
            {
                //Luz.areaSize = new Vector2(200f, 200f);
                Luz.intensity = 100f;
            }
            if (Tempo <= 0f)// Caso tenha acabado o tempo
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                ExplosionSound.Play();
                Debug.Log("KABOOM");
                GameObject Helice= (GameObject)Instantiate(Helice_Vento, transform.position, Quaternion.identity); // Instancia objeto
                Helice.GetComponent<HeliceDeVento>().enabled = true;// Ativa o movimento
                Helice.SetActive(true);
                Triggered = false;// Impede sucessivas explosões
                Triggered = true;
                Duration = 6f;
                Tempo = 10f;
                //Luz.areaSize = new Vector2(15f, 15f);
                Luz.intensity = 5f;
                Luz.enabled = false;
                this.gameObject.SetActive(false);

            }

        }
        else
        {
            Duration -= Time.deltaTime;
            if (Duration <= 1f)
            {
                //Luz.areaSize = new Vector2(200f, 200f);
                Luz.intensity = 100f;
            }
            if (Duration <= 0f)
            {
                //Luz.areaSize = new Vector2(15f, 15f);
                Luz.intensity = 5f;
                Duration = 6f;
                Tempo = 10f;
                Luz.enabled = false;
                Triggered = true;
                this.gameObject.SetActive(false);

            }

        }

    }
}

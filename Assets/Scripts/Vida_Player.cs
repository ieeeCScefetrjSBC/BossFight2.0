using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida_Player : MonoBehaviour
{
    public AudioSource DanoPlayer;

    private GameObject telaMorte;           // Objeto da tela de morte
    private GameObject mira;                // Objeto da Mira
    private CamMove    camMoveScript;       // Objeto da câmera principal
    private TiroPlayer tiroPlayerScript;
    public Slider Slider; //faz referência ao slider de health
    public float flashSpeed = 5f; // velocidade com que a cor vai aparecer na tela
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // cor em rgb
    public Image imagemDano;
    private float vidaMax = 1000;           // Vida Máxima do Player
    private float vida = 1000;              // Vida do Player
    private bool Dano;
    private float regenCooldown = 3f;       // Tempo para começar a se regenerar;
    private float regenFactor = 100f;       // Fator de regeneração

    private void Start()
    {
        telaMorte        = GameObject.FindGameObjectWithTag("Tela_Morte");    // Define qual é o objeto da tela de morte
        mira             = GameObject.FindGameObjectWithTag("Mira");
        camMoveScript    = Camera.main.GetComponent<CamMove>();               // Define quem é a Main Camera
        tiroPlayerScript = gameObject.GetComponent<TiroPlayer>();

        telaMorte.SetActive(false);
    }

    void Update()
    {
        if (Dano)
        {
            imagemDano.color = flashColour;
        }
        else
        {
            imagemDano.color = Color.Lerp(imagemDano.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        Dano = false;
        Slider.value = vida;

        if (vida <= 0)                                              // Verifica a vida do player
        {
            Debug.Log("ACABOOOU");                                  // Acabou

            Time.timeScale = 0;                                     // Pausa o tempo do jogo
            telaMorte.SetActive(true);                              // Ativa a tela de morte
            mira.SetActive(false);                                  // Remove a mira

            camMoveScript.enabled    = false;                       // Paralisa a movimentação da câmera
            tiroPlayerScript.enabled = false;                       // Paralisa o tiro do player

            if (Input.GetKeyDown(KeyCode.A))                        // Caso aperte A
            {
                Time.timeScale = 1;
                Debug.Log("vaisefode");
                SceneManager.LoadScene("Menu");                   // Volta para o menu inicial
            }
            
            Cursor.lockState = CursorLockMode.None;
        }

        if (regenCooldown <= 0)                        // Caso tenha passado 3 segundos em receber dano, começa a regenerar
        {
            vida += regenFactor * Time.deltaTime;      // Recupera 100 de HP por segundo
            if (vida > vidaMax)                       // Caso o regen tenha ultrapassado a vida máxima
                vida = vidaMax;
        }
        else
        {
            regenCooldown -= Time.deltaTime;           // Decai contador para começar a regenerar
        }
    }

    public void danoPlayer(float dano)      // Função que tira vida (chamada em outros scripts)
    {
        vida -= dano;
        DanoPlayer.Play();
        Dano = true;
        regenCooldown = 3f;                // Reseta contador para regenerar
    }

    public float getvida()                  //Getter da vida pra ser usado em outros scripts
    {
        return vida;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            Debug.Log("CAIIIIU");
            Debug.Log("vaisefode3");
            vida = 0;
            //SceneManager.LoadScene(nomeCena);
        }
    }
}

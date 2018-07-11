using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida_Player : MonoBehaviour
{
    public AudioSource DanoPlayer;

    private GameObject deathScreen;           // Objeto da tela de morte
    private Text Tempo_Derrota;             // Tempo em que o player foi derrotado
    private GameObject aim;                // Objeto da Mira
    private CamMove    camMoveScript;       // Objeto da câmera principal
    private Blaster blasterScript;
    private Image HealthBar; //faz referência à health bar
    public float flashSpeed = 5f; // velocidade com que a cor vai aparecer na tela
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); // cor em rgb
    private float vidaMax = 1000;           // Vida Máxima do Player
    private float vida = 1000;              // Vida do Player
    private bool Dano;
    private float regenCooldown = 3f;       // Tempo para começar a se regenerar;
    private float regenFactor = 100f;       // Fator de regeneração
    private float TimeSinceStart;

    private void Start()
    {
        deathScreen      = GameObject.FindGameObjectWithTag("Tela_Morte");    // Define qual é o objeto da tela de morte
        Tempo_Derrota = GameObject.FindGameObjectWithTag("Tempo_Derrota").GetComponent<Text>();
        aim              = GameObject.FindGameObjectWithTag("Mira");
        camMoveScript    = Camera.main.GetComponent<CamMove>();               // Define quem é a Main Camera
        blasterScript = gameObject.GetComponent<Blaster>();
        HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();
        deathScreen.SetActive(false);
        TimeSinceStart = 0;
    }

    void Update()
    {
        TimeSinceStart += Time.deltaTime;
        HealthBar.fillAmount = vida / 1000;
        HealthBar.color = Color.Lerp(Color.green, Color.red, 1 - HealthBar.fillAmount);
        if (vida <= 0)                                              // Verifica a vida do player
            ManageDeathScreen();
    }

    private void ManageDeathScreen()
    {
        //Debug.Log("ACABOOOU");                                  // Acabou

        Time.timeScale = 0;                                     // Pausa o tempo do jogo

        deathScreen.SetActive(true);                            // Ativa a tela de morte
        Tempo_Derrota.text = "Sobreviveu\npor  \n" + ((int)(TimeSinceStart / 60)).ToString() + "M e " + ((int)(TimeSinceStart % 60)).ToString() + "S";
        aim.SetActive(false);                                   // Remove a mira
        Cursor.lockState = CursorLockMode.None;

        camMoveScript.enabled = false;                       // Paralisa a movimentação da câmera
        blasterScript.enabled = false;                       // Paralisa o tiro do player

        if (Input.GetKeyDown(KeyCode.Return))                        // Caso aperte A
        {
            Debug.Log("vaisefode");

            Time.timeScale = 1;
            Cursor.visible = true;
            SceneManager.LoadScene("Menu");                   // Volta para o menu inicial
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
            //Debug.Log("CAIIIIU");
            vida = 0;
            //SceneManager.LoadScene(nomeCena);
        }
    }
}

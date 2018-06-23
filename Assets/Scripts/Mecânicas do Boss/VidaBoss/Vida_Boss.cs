using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vida_Boss : MonoBehaviour {

    // VARIÁVEIS PÚBLICAS
    public AudioSource DanoBoss;
    public float deathAnimTime = 15f;
    public float TimeToFirework = 5f;

    // VARIÁVEIS PRIVADAS
    private GameObject telaVitoria;
    private Text Tempo_Vitoria;
    private GameObject core;
    private Mascara_Script Mascara;
    private CamMove camMoveScript;       // Objeto da câmera principal
    private Blaster blasterScript;

    private float vida = 1000F;
    private float timeOfVictory = Mathf.Infinity;
    private float TimeSinceStart;
    private bool victory = false;

    // Use this for initialization
    private void Start()
    {
        telaVitoria = GameObject.FindGameObjectWithTag("Tela_Vitoria");
        Tempo_Vitoria = GameObject.FindGameObjectWithTag("Tempo_Vitoria").GetComponent<Text>();
        telaVitoria.SetActive(false);
        Mascara = GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>();
        camMoveScript = Camera.main.GetComponent<CamMove>();
        blasterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Blaster>();
        core = GameObject.Find("Core");

    }
    void Update () {
        float timeSinceVictory = Time.time - timeOfVictory;
        TimeSinceStart = Time.time;
        if (timeSinceVictory > deathAnimTime && victory)
        {
            ManageVictoryScreen();
        }
        else
        {
            if(timeSinceVictory > TimeToFirework && victory)
            {
                core.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (victory)
            {
                Light light = GameObject.FindGameObjectWithTag("PointLight").GetComponent<Light>();
                core.GetComponent<Animator>().enabled = true;
                light.intensity -= timeSinceVictory/50;
            }

            if (Mascara.GetBossMorto() && !victory)
            {
                Debug.Log("BOSS MORREU");

                timeOfVictory = Time.time;
                victory = true;
                GetComponent<Tiro_Boss>().enabled = false;
                GetComponent<Comp_Call>().enabled = false;
            }
        }
    }
    void ManageVictoryScreen()
    {
        //Debug.Log("ACABOOOU");                                  // Acabou

        Time.timeScale = 0;                                     // Pausa o tempo do jogo

        telaVitoria.SetActive(true);                            // Ativa a tela de morte
        Tempo_Vitoria.text = "Xibalba conquistada em  \n" + ((int)(TimeSinceStart/60)).ToString() + "M e " +  ((int)(TimeSinceStart%60 - 15)).ToString() + "S";
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

    public void danoBoss(float dano)
    {
        vida -= dano;
        DanoBoss.Play();
    }

    public float getvida()
    {
        return vida;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida_Player : MonoBehaviour
{
    private float Vida_Max = 1000;// Vida Máxima do Player
    private float vida = 1000;// Vida do Player
    private float Regen_Cooldown = 3f;// Tempo para começar a se regenerar;
    private float Regen_Factor = 100f;// Fator de regeneração
    public AudioSource DanoPlayer;
    string nomeCena = "Menu";
    private GameObject testepart;
    private GameObject Tela_Morte;// Objeto da tela de morte
    private GameObject Mira;// Objeto da Mira
    private Camera MainCamera;// Objeto da câmera principal

    private void Start()
    {
        Tela_Morte = GameObject.FindGameObjectWithTag("Tela_Morte");// Define qual é o objeto da tela de morte
        Tela_Morte.SetActive(false);
        Mira = GameObject.FindGameObjectWithTag("Mira");
        MainCamera = Camera.main;// Define quem é a Main Camera
    }

    void Update()
    {
        if (vida <= 0)                // Verifica a vida do player
        {
            Debug.Log("ACABOOOU");// Acabou
            Tela_Morte.SetActive(true);// Ativa a tela de morte
            Time.timeScale = 0;// Pausa o tempo do jogo
            MainCamera.GetComponent<CamMove>().enabled = false;// Paralisa a movimentação da câmera
            GetComponent<TiroPlayer>().enabled = false;// Paralisa o tiro do player
            Mira.SetActive(false);// Remove a mira
            if (Input.GetKeyDown(KeyCode.A))// Caso aperte A
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(nomeCena);// Volta para o menu inicial
            }           
            Cursor.lockState = CursorLockMode.None;
        }

        if (Regen_Cooldown <= 0) // Caso tenha passado 3 segundos em receber dano, começa a regenerar
        {
            vida += Regen_Factor * Time.deltaTime;// Recupera 100 de HP por segundo
            if (vida > Vida_Max)// Caso o regen tenha ultrapassado a vida máxima
                vida = Vida_Max;// 
        }
        else
        {
            Regen_Cooldown -= Time.deltaTime;// Decai contador para começar a regenerar
        }

    }

    public void danoPlayer(float dano) // Função que tira vida (chamada em outros scripts)
    {
        vida -= dano;
        DanoPlayer.Play();
        Regen_Cooldown = 3f;// Reseta contador para regenerar
    }

    public float getvida() //Getter da vida pra ser usado em outros scripts
    {
        return vida;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chao")
        {
            Debug.Log("CAIIIIU");
            vida = 0;
            //SceneManager.LoadScene(nomeCena);
        }
    }


}

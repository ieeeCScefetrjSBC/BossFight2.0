using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida_Player : MonoBehaviour
{
    private float Vida_Max = 1000;// Vida Máxima do Player
    private float vida = 1000;// Vida do Player
    private float Regen_Cooldown = 3f;// Tempo para começar a se regenerar;
    private float Regen_Factor = 100;// Fator de regeneração
    public AudioSource DanoPlayer;
    string nomeCena = "Menu";

    void Update()
    {
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
        if (vida <= 0)                // Verifica a vida do player
        {
            Debug.Log("ACABOOOU");
			SceneManager.LoadScene(nomeCena);
			Cursor.lockState = CursorLockMode.None;
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

    void OnControllerColliderHit(ControllerColliderHit hit) //Verifica colisão com o chão (BUGADO)
    {
        if (hit.gameObject.tag == "Chao")
        {
            Debug.Log("CAIIIIU");
            vida = 0;
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "ShurikenDeFogo")
        {
            danoPlayer(3);
            Debug.Log("KOE");
        }
    }
}


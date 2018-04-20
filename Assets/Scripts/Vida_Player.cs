using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida_Player : MonoBehaviour
{
    private int vida = 3;
    public AudioSource DanoPlayer;
    string nomeCena = "Menu";

    void Update()
    {
        if(vida <= 0)                // Verifica a vida do player
        {
            Debug.Log("ACABOOOU");
            Destroy(this.gameObject);
        }
    }

    public void danoPlayer(int dano) // Função que tira vida (chamada em outros scripts)
    {
        vida -= dano;
        DanoPlayer.Play();
    }

    public int getvida() //Getter da vida pra ser usado em outros scripts
    {
        return vida;
    }

    private void OnCollisionEnter(Collision collision) //Verifica colisão com o chão (BUGADO)
    {
        Debug.Log("CAIIIIU");

        if (collision.gameObject.tag == "Chao")
        {
            Debug.Log("CAIIIIU");
            vida = 0;
            SceneManager.LoadScene(nomeCena);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Part.LavaBall")
            danoPlayer(3);
    }
}
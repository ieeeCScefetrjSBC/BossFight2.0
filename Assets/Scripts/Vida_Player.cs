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

    void OnControllerColliderHit(ControllerColliderHit hit) //Verifica colisão com o chão (BUGADO)
    {
        if (hit.gameObject.tag == "Chao")
        {
            Debug.Log("CAIIIIU");
            vida = 0;
            SceneManager.LoadScene(nomeCena);
        }
    }

     void OnParticleCollision(GameObject other)
    {
        if (other.tag =="ShurikenDeFogo")
        {
            danoPlayer(3);
            Debug.Log("KOE");
        }
    }
}
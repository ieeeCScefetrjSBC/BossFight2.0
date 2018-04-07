using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Player : MonoBehaviour
{
    private int vida = 3;
    public AudioSource DanoPlayer;

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

    /*private void OnCollisionEnter(Collision collision) //Verifica colisão com o chão (BUGADO)
    {
        if (collision.gameObject.tag.Equals("Chao"))
        {
            Debug.Log("CAIIIIU");
            Destroy(this.gameObject);
        }
    }*/
}
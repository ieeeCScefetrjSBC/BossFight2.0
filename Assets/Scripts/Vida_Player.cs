using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Player : MonoBehaviour
{
    private int vida = 3;

    void Update()
    {
        if(vida < 0)
        {
            Debug.Log("ACABOOOU");
            Destroy(this.gameObject);
        }
    }

    public void danoPlayer(int dano)
    {
        vida -= dano;
    }
    public int getvida()
    {
        return vida;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Chao"))
        {
            Debug.Log("CAIIIIU");
            Destroy(this.gameObject);
        }
    }
}
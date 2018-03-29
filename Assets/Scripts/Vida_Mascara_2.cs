using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_2 : MonoBehaviour
{
    private int vida = 30; //Vida da máscara2

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.vida < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void setVida(int dano)
    {
        vida -= dano;
    }

    public int getVida()
    {
        return vida;
    }
}

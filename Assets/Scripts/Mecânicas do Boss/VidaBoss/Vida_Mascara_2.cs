using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_2 : MonoBehaviour
{
    private float vida = 4f; //Vida da máscara2

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.vida < 1f)
        {
            Destroy(this.gameObject);
        }
    }

    public void setVida(float dano)
    {
        vida -= dano;
    }

    public float getVida()
    {
        return vida;
    }
}

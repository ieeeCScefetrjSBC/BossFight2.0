﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_3 : MonoBehaviour
{
    private float vida = 4f; //Vida da máscara3

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
        if (GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>().GetActiveMask() == gameObject)
            vida -= dano;
    }

    public float getVida()
    {
        return vida;
    }
}

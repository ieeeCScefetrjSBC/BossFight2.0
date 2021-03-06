﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Vida_Mascara_2 : MonoBehaviour
{
    private float vida = 400; //Vida da máscara2
    private Image Mask2_Health; //faz referência à vida da mascara 2


    // Use this for initialization
    void Start()
    {
        Mask2_Health = GameObject.FindGameObjectWithTag("Mask2_Health").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        Mask2_Health.fillAmount = vida / 400;
        if (this.vida < 1f)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>().SetMasc2(false);
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

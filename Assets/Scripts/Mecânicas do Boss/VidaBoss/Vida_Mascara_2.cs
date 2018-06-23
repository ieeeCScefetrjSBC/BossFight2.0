using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_2 : MonoBehaviour
{
    private float vida = 400; //Vida da máscara2

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

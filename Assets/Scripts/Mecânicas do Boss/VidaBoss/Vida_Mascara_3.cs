using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_Mascara_3 : MonoBehaviour
{
    private float vida = 400; //Vida da máscara3
    private Image Mask3_Health; //faz referência à vida da mascara 3


    // Use this for initialization
    void Start()
    {
        Mask3_Health = GameObject.FindGameObjectWithTag("Mask3_Health").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        Mask3_Health.fillAmount = vida / 400;
        if (this.vida < 1f)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>().SetMasc3(false);
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

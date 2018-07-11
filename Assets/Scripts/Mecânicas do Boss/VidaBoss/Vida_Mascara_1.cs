using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida_Mascara_1 : MonoBehaviour 
{
    private float vida = 400; //Vida da máscara1
    private Image Mask1_Health; //faz referência à vida da mascara 1


    // Use this for initialization
    void Start() 
	{
        Mask1_Health = GameObject.FindGameObjectWithTag("Mask1_Health").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() 
	{
        Mask1_Health.fillAmount = vida / 400;
        if (this.vida < 1f)
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>().SetMasc1(false);
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

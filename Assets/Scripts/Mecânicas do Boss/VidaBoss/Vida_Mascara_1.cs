using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_1 : MonoBehaviour 
{
    private float vida = 4f; //Vida da máscara1

	// Use this for initialization
	void Start() 
	{
		
	}
	
	// Update is called once per frame
	void Update() 
	{
		if(this.vida < 1f)
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

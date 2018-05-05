using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Mascara_1 : MonoBehaviour 
{
    private float vida = 400f; //Vida da máscara1

	// Use this for initialization
	void Start() 
	{
		
	}
	
	// Update is called once per frame
	void Update() 
	{
		
		if(this.vida < 1f)
        {
            Destroy(this.gameObject);
        }
	}

    public void setVida(float dano)
    {
		if(gameObject!=null)
        vida -= dano;
    }

    public float getVida()
    {
		if (gameObject != null)
			return vida;
		else
			return 0;
    }
}

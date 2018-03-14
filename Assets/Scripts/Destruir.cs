using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour {

    public void Vida_Plataforma(float dano)
    {
        if(dano == 20)
        {
            Kill();
        }
    }

    // Use this for initialization
    public void Kill()
    {
        Destroy(gameObject);
    }
}

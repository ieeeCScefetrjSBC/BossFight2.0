using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Boss : MonoBehaviour {
    private int vida = 30;

    // Use this for initialization
    void Start () {
        if (vida < 0)
        {
            Debug.Log("BOSS MORREU");
            Destroy(this.gameObject);
        }
    }

    public void danoBoss(int dano)
    {
        vida -= dano;
    }

    public int getvida()
    {
        return vida;
    }
}

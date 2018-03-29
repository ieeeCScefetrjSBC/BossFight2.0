using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Boss : MonoBehaviour {
    private int vida = 30;
    public AudioSource DanoBoss;

    // Use this for initialization
    void Update () {
        if (vida < 0)
        {
            Debug.Log("BOSS MORREU");
            Destroy(this.gameObject);
        }
        Debug.Log(vida);
    }

    public void danoBoss(int dano)
    {
        vida -= dano;
        DanoBoss.Play();
    }

    public int getvida()
    {
        return vida;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Boss : MonoBehaviour {
    private float vida = 1000F;
    public AudioSource DanoBoss;

    private GameObject telaVitoria;

    // Use this for initialization
    private void Start()
    {
        telaVitoria        = GameObject.FindGameObjectWithTag("Tela_Vitoria");
        telaVitoria.SetActive(false);
    }
    void Update () {
        if (vida < 0)
        {
            Debug.Log("BOSS MORREU");
            Destroy(this.gameObject);

            telaVitoria.SetActive(true);
        }
    }

    public void danoBoss(float dano)
    {
        vida -= dano;
        DanoBoss.Play();
    }

    public float getvida()
    {
        return vida;
    }
}

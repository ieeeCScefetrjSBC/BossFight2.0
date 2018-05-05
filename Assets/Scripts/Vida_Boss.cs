using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vida_Boss : MonoBehaviour {
    private float vida = 1000F;
    public AudioSource DanoBoss;

    // Use this for initialization
    void Update () {
        if (vida < 0)
        {
            Debug.Log("BOSS MORREU");
            //Destroy(this.gameObject);
			SceneManager.LoadScene("Menu");
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

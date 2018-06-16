using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Boss : MonoBehaviour {

    // VARIÁVEIS PÚBLICAS
    public AudioSource DanoBoss;
    public float deathAnimTime = 5f;

    // VARIÁVEIS PRIVADAS
    private GameObject telaVitoria;
    private GameObject core;
    private Mascara_Script Mascara;

    private float vida = 1000F;
    private float timeOfVictory = Mathf.Infinity;
    private bool victory = false;

    // Use this for initialization
    private void Start()
    {
        telaVitoria = GameObject.FindGameObjectWithTag("Tela_Vitoria");
        telaVitoria.SetActive(false);
        Mascara = GameObject.FindGameObjectWithTag("Boss").GetComponent<Mascara_Script>();
        core = GameObject.Find("Core");

    }
    void Update () {
        float timeSinceVictory = Time.time - timeOfVictory;

        if (timeSinceVictory > deathAnimTime && victory)
        {
            telaVitoria.SetActive(true);
        }

        if (victory)
        {
            Light light = core.GetComponent<Light>();
            light.enabled = true;
            light.intensity = timeSinceVictory;
        }

        if (Mascara.BossMorto && !victory)
        {
            Debug.Log("BOSS MORREU");

            core.GetComponent<MeshRenderer>().enabled = false;

            timeOfVictory = Time.time;
            victory = true;
        }
        //if (vida < 0)
        //{
        //    Debug.Log("BOSS MORREU");
        //    Destroy(this.gameObject);

        //    telaVitoria.SetActive(true);
        //}
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

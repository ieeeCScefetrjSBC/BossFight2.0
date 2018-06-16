using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_Script : MonoBehaviour {
    private GameObject player;
    private GameObject mascara_1;
    private GameObject mascara_2;
    private GameObject mascara_3;
    private GameObject core;
    private GameObject boss;
    private Vida_Boss vidaBoss;
    private bool setMascara = true;

    private Quaternion rotateQuat;
    public float speed = 1f;

    private GameObject particle_2;
    private GameObject particle_3;

    public bool Masc1;
    public bool Masc2;
    public bool Masc3;
    public bool BossMorto;


    // Use this for initialization 
    void Start () {
        vidaBoss = this.gameObject.GetComponent<Vida_Boss>();
        player = GameObject.FindGameObjectWithTag("Player");
        mascara_1 = GameObject.FindGameObjectWithTag("Mascara1"); //Objeto mascara 1 atribuido
        mascara_2 = GameObject.FindGameObjectWithTag("Mascara2"); //Objeto mascara 2 atribuido
        mascara_3 = GameObject.FindGameObjectWithTag("Mascara3"); //Objeto mascara 3 atribuido
        core = GameObject.Find("Core");
        boss = GameObject.FindGameObjectWithTag("Boss");

        Masc1 = true;
        Masc2 = true;
        Masc3 = true;
        BossMorto = false;

        particle_2 = GameObject.FindGameObjectWithTag("particle2");
        particle_3 = GameObject.FindGameObjectWithTag("particle3");
        particle_2.SetActive(false);
        particle_3.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if(mascara_1 == null)
        {
            Masc1 = false;
            Debug.Log("M1 MORREUU");
        }
        if (mascara_2 == null)
        {
            Masc2 = false;
            Debug.Log("M2 MORREUU");
        }
        if (mascara_3 == null)
        {
            Masc3 = false;
            Debug.Log("M3 MORREUU");
        }
        if(!Masc1 && !Masc2 && !Masc3)
        {
            BossMorto = true;
        }


        float vida = vidaBoss.getvida();
        
        if (Masc1)
        {
            //Boss vira na direçao do player
            
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

            //Faz a mascara 1 ser a principal
            if (setMascara == true)
            {
                transform.DetachChildren();
                //transform.LookAt(mascara_1.transform);
                transform.rotation = Quaternion.LookRotation(mascara_1.transform.forward, mascara_1.transform.up);
                
                mascara_1.transform.SetParent(this.transform);
                mascara_2.transform.SetParent(this.transform);
                mascara_3.transform.SetParent(this.transform);
                core.transform.SetParent(this.transform);
                setMascara = false;
            }
        }
        else if(Masc2)
        {
            //Boss vira na direçao do player
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

            //Faz a mascara 2 ser a principal
            if (setMascara == false)
            {
                particle_2.SetActive(true);
                transform.DetachChildren();
                transform.rotation = Quaternion.LookRotation(mascara_2.transform.forward, mascara_2.transform.up);

                mascara_2.transform.SetParent(this.transform);
                mascara_3.transform.SetParent(this.transform);
                core.transform.SetParent(this.transform);
                setMascara = true;
            }
        }
        else if(Masc3)
        {
            //Boss vira na direçao do player
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

            //Faz a mascara 3 ser a principal
            if (setMascara == true)
            {
                particle_3.SetActive(true);
                transform.DetachChildren();
                transform.rotation = Quaternion.LookRotation(mascara_3.transform.forward, mascara_3.transform.up);

                mascara_3.transform.SetParent(this.transform);
                core.transform.SetParent(this.transform);
                setMascara = true;
            }
        }

        else
        {
            //Destroy(boss);
        }


    }
}

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

    private GameObject[] masks = new GameObject[3];
    private GameObject[] particles = new GameObject[3];
    private GameObject activeMask;
    private bool[] masksOn = new bool[3];
    private int currentMaskIdx = 0;
    private bool maskSet = false;


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


        masks[0] = GameObject.FindGameObjectWithTag("Mascara1");
        masks[1] = GameObject.FindGameObjectWithTag("Mascara2");
        masks[2] = GameObject.FindGameObjectWithTag("Mascara3");
        activeMask = masks[0];

        particles[0] = GameObject.FindGameObjectWithTag("particle1");
        particles[1] = GameObject.FindGameObjectWithTag("particle2");
        particles[2] = GameObject.FindGameObjectWithTag("particle3");

        particles[0].SetActive(false);
        particles[1].SetActive(false);
        particles[2].SetActive(false);

        //masksOn[0] = Masc1;
        //masksOn[1] = Masc2;
        //masksOn[2] = Masc3;
    }

    public GameObject GetActiveMask()
    {
        return activeMask;
    }

    public void ChooseMask(int maskNum)
    {
        GameObject mask;
        int maskIdx = maskNum - 1;

        if (maskNum < 0 || maskNum > 2)
            Debug.Log("ESSA MASCARA NAO EXISTE OTARIO");

        //masksOn[currentMaskIdx] = false;
        //masksOn[maskIdx] = true;

        mask = masks[maskIdx];
        activeMask = mask;
        //currentMaskIdx = maskIdx;

        transform.DetachChildren();
        transform.rotation = Quaternion.LookRotation(mask.transform.forward, mask.transform.up);
        //particles[maskNum-1].SetActive(true);
        core.transform.SetParent(this.transform);

        for (int idx = 0; idx < 3; idx++)
        {
            if (masks[idx] != null)
                masks[idx].transform.SetParent(this.transform);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChooseMask(1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChooseMask(2);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChooseMask(3);

        if (mascara_1 == null)
        {
            Masc1 = false;
            maskSet = false;
            Debug.Log("M1 MORREUU");
        }
        if (mascara_2 == null)
        {
            Masc2 = false;
            maskSet = false;
            Debug.Log("M2 MORREUU");
        }
        if (mascara_3 == null)
        {
            Masc3 = false;
            maskSet = false;
            Debug.Log("M3 MORREUU");
        }
        if(!Masc1 && !Masc2 && !Masc3)
        {
            BossMorto = true;
        }

        float vida = vidaBoss.getvida();
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

        if (!maskSet)
        {
            if (Masc1)
                ChooseMask(1);
            else if (Masc2)
                ChooseMask(2);
            else if (Masc3)
                ChooseMask(3);

            maskSet = true;
        }

        //if (Masc1)
        //{
        //    //Boss vira na direçao do player

        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

        //    //Faz a mascara 1 ser a principal
        //    if (setMascara == true)
        //    {
        //        transform.DetachChildren();
        //        //transform.LookAt(mascara_1.transform);
        //        transform.rotation = Quaternion.LookRotation(mascara_1.transform.forward, mascara_1.transform.up);

        //        mascara_1.transform.SetParent(this.transform);
        //        mascara_2.transform.SetParent(this.transform);
        //        mascara_3.transform.SetParent(this.transform);
        //        core.transform.SetParent(this.transform);
        //        setMascara = false;
        //    }
        //}
        //else if (Masc2)
        //{
        //    //Boss vira na direçao do player
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

        //    //Faz a mascara 2 ser a principal
        //    if (setMascara == false)
        //    {
        //        particle_2.SetActive(true);
        //        transform.DetachChildren();
        //        transform.rotation = Quaternion.LookRotation(mascara_2.transform.forward, mascara_2.transform.up);

        //        mascara_2.transform.SetParent(this.transform);
        //        mascara_3.transform.SetParent(this.transform);
        //        core.transform.SetParent(this.transform);
        //        setMascara = true;
        //    }
        //}
        //else if (Masc3)
        //{
        //    //Boss vira na direçao do player
        //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(this.transform.position - player.transform.position), Time.time * speed);

        //    //Faz a mascara 3 ser a principal
        //    if (setMascara == true)
        //    {
        //        particle_3.SetActive(true);
        //        transform.DetachChildren();
        //        transform.rotation = Quaternion.LookRotation(mascara_3.transform.forward, mascara_3.transform.up);

        //        mascara_3.transform.SetParent(this.transform);
        //        core.transform.SetParent(this.transform);
        //        setMascara = true;
        //    }
        //}
    }
}

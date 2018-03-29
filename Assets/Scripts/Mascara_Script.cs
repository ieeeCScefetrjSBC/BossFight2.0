using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_Script : MonoBehaviour {
    private GameObject player;
    private GameObject mascara_1;
    private GameObject mascara_2;
    private GameObject mascara_3;
    private Vida_Boss vidaBoss;
    private bool setMascara = true;
    // Use this for initialization
    void Start () {
        vidaBoss = this.gameObject.GetComponent<Vida_Boss>();
        player = GameObject.FindGameObjectWithTag("Player");
        mascara_1 = GameObject.FindGameObjectWithTag("Mascara1"); //Objeto mascara 1 atribuido
        mascara_2 = GameObject.FindGameObjectWithTag("Mascara2"); //Objeto mascara 2 atribuido
        mascara_3 = GameObject.FindGameObjectWithTag("Mascara3"); //Objeto mascara 3 atribuido
    }
	
	// Update is called once per frame
	void Update () {

        int vida = vidaBoss.getvida();
        
        if (mascara_1 != null)
        {
            //Boss vira na direçao do player
            transform.LookAt(player.transform);
            //Faz a mascara 1 ser a principal
            if (setMascara == true)
            {
                this.transform.DetachChildren();
                transform.LookAt(mascara_1.transform);
                mascara_1.transform.SetParent(this.transform);
                mascara_2.transform.SetParent(this.transform);
                mascara_3.transform.SetParent(this.transform);
                setMascara = false;
            }
        }
        else if(mascara_2 != null)
        {
            //Boss vira na direçao do player
            transform.LookAt(player.transform);
            //Faz a mascara 2 ser a principal
            if (setMascara == false)
            {
                this.transform.DetachChildren();
                transform.LookAt(mascara_2.transform);
                //mascara_1.transform.SetParent(this.transform);
                mascara_2.transform.SetParent(this.transform);
                mascara_3.transform.SetParent(this.transform);
                setMascara = true;
            }
        }
        else
        {
            //Boss vira na direçao do player
            transform.LookAt(player.transform);
            //Faz a mascara 2 ser a principal
            if (setMascara == true)
            {
                this.transform.DetachChildren();
                transform.LookAt(mascara_3.transform);
                //mascara_1.transform.SetParent(this.transform);
                //mascara_2.transform.SetParent(this.transform);
                mascara_3.transform.SetParent(this.transform);
                setMascara = false;
            }
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_Script : MonoBehaviour {
    private GameObject player;
    private GameObject mascara_1;
    private GameObject mascara_2;
    private GameObject mascara_3;
    private Vida_Boss vidaBoss;
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
        
        if (vida > 20)
        {
            //Boss vira na direçao do player usando a Mascara1 como principal
            this.transform.rotation = Quaternion.LookRotation(((player.transform.position - transform.position) + (player.transform.position - mascara_1.transform.position)).normalized);
        }
        else if(vida > 10)
        {
            //Boss vira na direçao do player usando a Mascara2 como principal
            this.transform.rotation = Quaternion.LookRotation(((player.transform.position - transform.position) + (player.transform.position - mascara_2.transform.position)).normalized);
        }
        else if(vida > 0)
        {
            //Boss vira na direçao do player usando a Mascara3 como principal
            this.transform.rotation = Quaternion.LookRotation(((player.transform.position - transform.position) + (player.transform.position - mascara_3.transform.position)).normalized);
        }


    }
}

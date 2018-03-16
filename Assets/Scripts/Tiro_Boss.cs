﻿using UnityEngine;

public class Tiro_Boss : MonoBehaviour {
    private float fireRate = 1;                  //Quanto menor o fire rate mais tempo entre os tiros do boss 
    private float tempoAtirar = 0f;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= tempoAtirar)
        {
            tempoAtirar = Time.time + 1f / fireRate;
            Atirar();
        }

    }

    void Atirar ()
    {
        GameObject tiro = (GameObject)Instantiate(Resources.Load("Tiro"), transform.position, Quaternion.identity);

        tiro.GetComponent<Rigidbody>().velocity = (player.transform.position - tiro.transform.position);
        
    }

}

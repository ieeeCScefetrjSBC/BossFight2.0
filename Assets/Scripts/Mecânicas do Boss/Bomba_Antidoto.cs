using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Antidoto : MonoBehaviour {

    private bool Colidir = false;
    private GameObject PlayerOBJ;// Objeto do jogador na cena

    // Use this for initialization
    void Start() {
        PlayerOBJ = GameObject.FindGameObjectWithTag("Player");// Encontra o objeto via tag
    }

    // Update is called once per frame
    void Update() {
        



    }

    private void OnControllerColliderHit(ControllerColliderHit hit)//checa se o player colidiu com a bomba antidoto
    {
        if (hit.gameObject.name == "bomba_antidoto" || hit.gameObject.name == "bomba_antidoto (1)")
        {
            Colidir = true;
            Debug.Log("Colidiu");
        }
        else
        {
            Colidir = false;
        }
    }
}

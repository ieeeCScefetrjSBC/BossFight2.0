using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_Fire : MonoBehaviour
{

    private GameObject Ground;
    private ParticleSystem Fire;
    private float TimerGround = 0f;
    private float TimeAir = 0f;
    private GameObject Player;
    private GameObject Grounder;
    public GameObject Pilar;

    void Start()
    {

        Grounder = GameObject.FindGameObjectWithTag("Grounder"); //Identifica o objeto Grounder
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        TimeAir -= Time.deltaTime;
        if (Grounder.GetComponent<Grounded>().getGrounded() && Player.transform.position.x <= (transform.position.x + transform.localScale.x / 2) &&
            Player.transform.position.x >= (transform.position.x - transform.localScale.x / 2) && Player.transform.position.z <= (transform.position.z + transform.localScale.z / 2) &&
            Player.transform.position.z >= (transform.position.z - transform.localScale.z / 2) ) //Enquanto o player não sai do chão, inicia o timer 1
            
            {
            TimerGround += Time.deltaTime;
            TimeAir = 1f;
            Debug.Log("batata");
        }
        if (!Grounder.GetComponent<Grounded>().getGrounded() && TimeAir <= 0f)
        {
            TimerGround = 0f;
        }

        if (TimerGround > 10f)
        {
            GameObject Pilar_1 = Instantiate(Pilar, new Vector3(10, 0, 30), Quaternion.identity);
            GameObject Pilar_2 = Instantiate(Pilar, new Vector3(-10, 0, 30), Quaternion.identity);
        }

    }
}
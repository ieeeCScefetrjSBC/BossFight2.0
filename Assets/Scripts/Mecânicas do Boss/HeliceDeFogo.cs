using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeFogo : MonoBehaviour {
    private GameObject Player;
    private Vector3 Mov_Direção;
    public float Mov_Vel;
    public float Mov_Vel_Perto;
    public float Vel_Rotx, Vel_Roty, Vel_Rotz;
    private Vida_Player Vida_Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vida_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida_Player>();
    }

    void Update()
    {
        Mov_Direção = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - transform.position;
        transform.Rotate(Vector3.right * Time.deltaTime * Vel_Rotx, Space.World);   // rotação em x
        transform.Rotate(Vector3.up * Time.deltaTime * Vel_Roty, Space.World);   // rotação em y
        transform.Rotate(Vector3.forward * Time.deltaTime * Vel_Rotz, Space.World);   // rotação em z
        if ((Player.transform.position - transform.position).magnitude >= 4)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Mov_Direção.normalized * Mov_Vel, ForceMode.VelocityChange);
            if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= Mov_Vel)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity.normalized * Mov_Vel;
            }

            //transform.Translate(Mov_Direção.normalized * Mov_Vel * Time.deltaTime, Space.World);
            Debug.Log("Fugiu");
        }
        else
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Mov_Direção.normalized * Mov_Vel_Perto, ForceMode.VelocityChange);
            if(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= Mov_Vel_Perto)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity.normalized * Mov_Vel_Perto;
            }
            
            //transform.Translate(new Vector3(Player.transform.position.x - transform.position.x, 0, Player.transform.position.z - transform.position.z) * Mov_Vel_Perto * Time.deltaTime, Space.World);
            Debug.Log("DANOUSE");
            //Vida_Player.danoPlayer(1);
        }
    }
    void OnParticleCollision(GameObject other)
    {
        Rigidbody teste = other.GetComponent<Rigidbody>();
        if (teste)
        {
            Vida_Player.danoPlayer(3);
            Debug.Log("KOE");
        }
    }
}


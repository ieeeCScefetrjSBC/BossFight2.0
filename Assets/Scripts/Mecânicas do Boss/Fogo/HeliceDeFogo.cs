using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeFogo : MonoBehaviour {
    private GameObject Player;
    private Vector3 Mov_Direção;
    [SerializeField] private float Vel_Rot;
    [SerializeField] private float Aceleração;
    [SerializeField] private float Vel_Max;
    [SerializeField] private float Impulso_Explosão;
    [SerializeField] private float Tempo_de_Vida;   // Tempo para auto destruição
    private bool Longe = true;
    private bool Encostou = false;
    private bool VelMax_Atingida = false;
    private Vida_Player Vida_Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vida_Player = Player.GetComponent<Vida_Player>();
    }

    void Update()
    {
        if(Tempo_de_Vida > 0)
        {
            Tempo_de_Vida -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
        transform.Rotate(Vector3.up * Time.deltaTime * Vel_Rot, Space.World);   // rotação em y
        Mov_Direção = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - transform.position;
        if ((Player.transform.position - transform.position).magnitude <= 3.5f)
        {
            Longe = false;
            Encostou = true;
            //Debug.Log(Encostou);
        }
        if(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= Vel_Max)
        {
            VelMax_Atingida = true;
        }
        else
        {
            VelMax_Atingida = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (Longe)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Mov_Direção.normalized * Aceleração, ForceMode.VelocityChange);
            if (VelMax_Atingida)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Mov_Direção.normalized * Vel_Max;
            }
        }
        if (Encostou)
        {
            Destroy(this.gameObject);
            Player.GetComponent<Rigidbody>().AddForce(((Mov_Direção.normalized) + new Vector3(0, 1f, 0)) * Impulso_Explosão, ForceMode.VelocityChange);
            Player.gameObject.GetComponent<Vida_Player>().danoPlayer(100);
            //Debug.Log("Atingiu");
            Encostou = false;

        }

    }


}


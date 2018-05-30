using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeVento : MonoBehaviour {

    private GameObject Player;
    private Vector3 Mov_Direção; //Vetor direção do movimento
    private Vector3 direction; // Vetor direção da força
    [SerializeField] private float Aceleração; // Aceleração 
    [SerializeField] private float Vel_Max;    // Velocidade máxima de movimento
    [SerializeField] private float Vel_Rot;    // Rotação da helice
    [SerializeField] private float ForçaDoSopro; // Força do Sopro
    [SerializeField] private float DistanciaDeAtivação; // Distância em que o sopro da hélice é ativado.
    [SerializeField] private float Impulso_Tornado;     // Força com que o player é jogado ao alto
    private bool Longe = true;
    private bool Encostou = false;
    private bool Ativar_Sopro = false;      // Verifica se pode ou não ativar o sopro da helice de vento
    private bool VelMax_Atingida = false;
    private Vida_Player Vida_Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vida_Player = Player.GetComponent<Vida_Player>();
    }

    void Update()
    {
        // MOVIMENTO DA HELICE!!!
        Mov_Direção = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - transform.position;  //Direção de movimento da helice
        transform.Rotate(Vector3.up * Time.deltaTime * Vel_Rot, Space.World);   // rotação em y
        if ((Player.transform.position - transform.position).magnitude <= 3.5)     // Verifica se está perto ou distante do player
        {
            Longe = false;
            Encostou = true;
        }
        if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= Vel_Max)
        {
            VelMax_Atingida = true;
        }
        else
        {
            VelMax_Atingida = false;
        }

        // SOPRO DA HELICE DE VENTO
        direction = transform.position - Player.transform.position;  // Direção do player à helice
        if (direction.magnitude <= DistanciaDeAtivação)
        {
            Ativar_Sopro = true;
        }
        else
        {
            Ativar_Sopro = false;
        }
    }

    private void FixedUpdate()
    {
        if (Longe)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Mov_Direção.normalized * Aceleração, ForceMode.VelocityChange);
            if (VelMax_Atingida)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Mov_Direção.normalized * Aceleração;
            }
        }
        if (Encostou)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Aceleração = 0;
            Destroy(this.gameObject, 5);
            Player.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0).normalized * Impulso_Tornado , ForceMode.VelocityChange);
            Player.gameObject.GetComponent<Vida_Player>().danoPlayer(100);
            Debug.Log("Atingiu");
            Encostou = false;
        }

        ////// Sopro da helice de vento
        if (Ativar_Sopro)
        {
            Player.GetComponent<Rigidbody>().AddForce(direction.normalized * ForçaDoSopro, ForceMode.Force);
            Debug.Log("Sopro Ativado");
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeVento : MonoBehaviour {

    private GameObject Player;
    private Vector3 Mov_Direção; //Vetor direção do movimento
    private Vector3 direction; // Vetor direção da força
    [SerializeField] private float Mov_Vel;
    [SerializeField] private float Mov_Vel_Perto;
    [SerializeField] private float Vel_Rotx, Vel_Roty, Vel_Rotz;
    [SerializeField] private float ForceMultiplier; // Multiplicador da força dependente da distância entre player e helice
    [SerializeField] private float DistanciaDeAtivação; // Distância em que o sopro da hélice é ativado.
    private Vida_Player Vida_Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vida_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida_Player>();
        ForceMultiplier = 0;
    }

    void Update()
    {
        // MOVIMENTO DA HELICE!!!
        Mov_Direção = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - transform.position;  //Direção de movimento da helice
        transform.Rotate(Vector3.right * Time.deltaTime * Vel_Rotx, Space.World);   // rotação em x
        transform.Rotate(Vector3.up * Time.deltaTime * Vel_Roty, Space.World);   // rotação em y
        transform.Rotate(Vector3.forward * Time.deltaTime * Vel_Rotz, Space.World);   // rotação em z
        if ((Player.transform.position - transform.position).magnitude >= 4)     // Verifica se está perto ou distante do player
        {
            transform.Translate(Mov_Direção.normalized * Mov_Vel * Time.deltaTime, Space.World);

        }
        else
        {
            transform.Translate(new Vector3(Player.transform.position.x - transform.position.x, 0, Player.transform.position.z - transform.position.z) * Mov_Vel_Perto * Time.deltaTime, Space.World);

            //Vida_Player.danoPlayer(1);
        }

        // SOPRO DA HELICE DE VENTO
        direction = transform.position - Player.transform.position;  // Direção do player à helice
        if (direction.magnitude <= DistanciaDeAtivação)
        {
            Player.GetComponent<CharacterController>().Move(direction.normalized * ForceMultiplier);

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

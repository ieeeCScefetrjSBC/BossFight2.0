using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Spray : MonoBehaviour {

	// Use this for initialization
    private float Radius=0; // Raio da esfera
    private float Teta=0; // Ângulo horizontal
    private float Phi=0; // Ângulo Vertical
    private float Tempo=0f;
    private Vector3 Finale; // Onde o tiro está mirando
    private Vector3 Moving;  // Progressão do movimento
    private Vector3 Direction; // Direção do movimento
    private GameObject Player; // Objeto do Jogador
   
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player"); // Define GameObject Player
        Teta = Random.Range(0, Mathf.PI * 2f);
        Phi = Random.Range(0, Mathf.PI);
        Radius = Random.Range(0, 30f);
        Finale.x = Player.transform.position.x + Radius * Mathf.Cos(Teta) * Mathf.Sin(Phi); // Posição X do Jogador + Coordenada Esférica
        Finale.y = Player.transform.position.y + Radius * Mathf.Sin(Teta) * Mathf.Sin(Phi);// Posição Y do Jogador + Coordenada Esférica
        Finale.z = Player.transform.position.z + Radius * Mathf.Cos(Phi);// Posição Z do Jogador + Coordenada Esférica
        Direction = (Finale-transform.position).normalized;
    }
	
	// Update is called once per frame
	void Update () {
        Tempo += Time.deltaTime;
        
        transform.Translate(Direction*Time.deltaTime*50f);
	}
}

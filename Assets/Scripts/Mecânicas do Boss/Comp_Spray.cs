using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Spray : MonoBehaviour {

	// Use this for initialization
    private float Radius=0; // Raio da esfera
    private float Teta=0; // Ângulo horizontal
    private float Phi=0; // Ângulo Vertical
    private float Speed = 50f;// Velocidade do tiro
    private Vector3 Origin; // Origem do Tiro
    private Vector3 Finale; // Onde o tiro está mirando
    private Vector3 Center; // Vetor que guarda a posição do player no momento que o tiro é lançado
    private Vector3 Direction; // Direção do movimento
    private GameObject Player; // Objeto do Jogador
   
	void Start () {
        Origin = transform.position;
        Player = GameObject.FindGameObjectWithTag("Player"); // Define GameObject Player
        Teta = Random.Range(0, Mathf.PI * 2f); // Ângulo horizontal aleatório
        Phi = Random.Range(0, Mathf.PI);// Ângulo vertical aleatório
        Radius = Random.Range(0, 30f);// Distância do player aleatória
        Finale.x = Player.transform.position.x + Radius * Mathf.Cos(Teta) * Mathf.Sin(Phi); // Posição X do Jogador + Coordenada Esférica
        Finale.y = Player.transform.position.y + Radius * Mathf.Sin(Teta) * Mathf.Sin(Phi);// Posição Y do Jogador + Coordenada Esférica
        Finale.z = Player.transform.position.z + Radius * Mathf.Cos(Phi);// Posição Z do Jogador + Coordenada Esférica
        Center = Player.transform.position;
        Direction = (Finale-transform.position).normalized;// Direção do movimento definida como ponto de destino - ponto de origem
    }
	
	// Update is called once per frame
	void Update () {
        if ((Center - Origin).magnitude > (transform.position - Origin).magnitude)
            transform.Translate((Direction+(Center-transform.position).normalized/1f)*Time.deltaTime*Speed); // Muda a posição do objeto no vetor Direction e converge para a posição do player no momento do lançamento
        else
            transform.Translate(Direction * Time.deltaTime * Speed); // Muda a posição do objeto no vetor Direction, usando escala temporal e com velocidade "Speed"
    }
    private void OnCollisionEnter(Collision collision) //Verifica em quem está batendo
    {

        if (collision.gameObject.tag.Equals("Player")) //Se for player ele tira vida
        {
            collision.gameObject.GetComponent<Vida_Player>().danoPlayer(1);
            Debug.Log("Atingiu");
            Destroy(this.gameObject);
            
        }
       
    }
}

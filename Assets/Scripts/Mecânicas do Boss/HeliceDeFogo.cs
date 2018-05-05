using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeFogo : MonoBehaviour {
    public GameObject Player;
    public Transform helice;
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
		Mov_Direção = new Vector3 (Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - helice.transform.position;
		helice.transform.Rotate (Vector3.right * Time.deltaTime * Vel_Rotx, Space.World);   // rotação em x
		helice.transform.Rotate (Vector3.up * Time.deltaTime * Vel_Roty, Space.World);   // rotação em y
		helice.transform.Rotate (Vector3.forward * Time.deltaTime * Vel_Rotz, Space.World);   // rotação em z
		if ((Player.transform.position - transform.position).magnitude >= 4){
			helice.transform.Translate (Mov_Direção.normalized * Mov_Vel * Time.deltaTime, Space.World);
		Debug.Log ("Fugiu");}
        else
        {
            helice.transform.Translate(new Vector3(Player.transform.position.x - transform.position.x, 0, Player.transform.position.z - transform.position.z) * Mov_Vel_Perto * Time.deltaTime, Space.World);
            Debug.Log("DANOUSE");
			Vida_Player.danoPlayer(0.2f);
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


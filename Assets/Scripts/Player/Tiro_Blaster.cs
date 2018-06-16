using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro_Blaster : MonoBehaviour {

    Vector3 Direction;
    private GameObject Destruct_Particle;
    private GameObject mascaraFogo;
    private GameObject mascaraTempestade;
    private GameObject mascaraAgua;
    [SerializeField] private float Tiro_Speed;

	void Start () {
        this.gameObject.GetComponent<Rigidbody>().velocity = Direction.normalized * Tiro_Speed;
        Destruct_Particle = GameObject.FindGameObjectWithTag("Hitmark");
        mascaraFogo = GameObject.FindGameObjectWithTag("Mascara1");
        mascaraTempestade = GameObject.FindGameObjectWithTag("Mascara2");
        mascaraAgua = GameObject.FindGameObjectWithTag("Mascara3");
    }
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitmark = (GameObject)GameObject.Instantiate(Destruct_Particle, transform.position, Quaternion.identity);
        hitmark.GetComponent<HitMark>().enabled = true;
        Destroy(gameObject);
        if(collision.gameObject.tag == "Mascara1")
        {
            Debug.Log("OPAA");
            if (mascaraFogo != null)
            {
                mascaraFogo.GetComponent<Vida_Mascara_1>().setVida(2f);
                Debug.Log("OPA");
            }
        }
        if (collision.gameObject.tag == "Mascara2")
        {
            if (mascaraTempestade != null)
            {
                mascaraTempestade.GetComponent<Vida_Mascara_2>().setVida(2f);
                Debug.Log("OPA");
            }
        }
        if (collision.gameObject.tag == "Mascara3")
        {
            if (mascaraAgua != null)
            {
                mascaraAgua.GetComponent<Vida_Mascara_3>().setVida(2f);
                Debug.Log("OPA");
            }
        }
        Debug.Log("Ativa particula");
    }

    public void set_Direction(Vector3 direction)
    {
        Direction = direction;
        return;
    }
}

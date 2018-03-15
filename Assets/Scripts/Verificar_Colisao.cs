using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verificar_Colisao : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Player"))
        {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag.Equals("Plataforma")){
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}

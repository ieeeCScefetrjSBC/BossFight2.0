using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    private Camera MainCamera;
    private GameObject Ponta_Arma;
    private GameObject Tiro;
    private Vector3 Direction;

	void Start () {
        Tiro = GameObject.FindGameObjectWithTag("Tiro_Player");
        Ponta_Arma = GameObject.FindGameObjectWithTag("Arma");
        MainCamera = Camera.main;
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Atira();
        }

    }

    void Atira()
    {
        RaycastHit Hit;


        Debug.Log("OPA");
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward))
        {
            GameObject Projetil = (GameObject)GameObject.Instantiate(Tiro, Ponta_Arma.transform.position + Ponta_Arma.transform.forward.normalized * 2, Quaternion.Euler(MainCamera.transform.rotation.eulerAngles + new Vector3(90,0,0)));
            Projetil.GetComponent<Tiro_Blaster>().set_Direction(MainCamera.transform.forward);
        }
    }
}

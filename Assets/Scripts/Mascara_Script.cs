using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascara_Script : MonoBehaviour {
    private GameObject mascara_1;
    private GameObject mascara_2;
    private GameObject mascara_3;
    // Use this for initialization
    void Start () {
        mascara_1 = GameObject.FindGameObjectWithTag("Mascara1"); //Objeto mascara 1 atribuido
        mascara_2 = GameObject.FindGameObjectWithTag("Mascara2"); //Objeto mascara 2 atribuido
        mascara_3 = GameObject.FindGameObjectWithTag("Mascara3"); //Objeto mascara 3 atribuido
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.right * Time.deltaTime);
        transform.Rotate(Vector3.up * Time.deltaTime, Space.World);
    }
}

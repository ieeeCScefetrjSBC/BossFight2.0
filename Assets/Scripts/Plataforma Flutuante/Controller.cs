using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private Vector3 PosiçãoInicial;
    private Quaternion RotaçãoInicial;
    private GameObject PlataformaFlutuante;
    private GameObject Plataforma_Exemplo;

	void Start ()
    {
        PlataformaFlutuante = transform.GetChild(0).gameObject;
        PosiçãoInicial = PlataformaFlutuante.transform.position;
        RotaçãoInicial = PlataformaFlutuante.transform.rotation;
        Plataforma_Exemplo = GameObject.FindGameObjectWithTag("Plataforma_Exemplo");
    }
	
	void Update ()
    {
        if(PlataformaFlutuante == null)
        {
            PlataformaFlutuante = (GameObject)GameObject.Instantiate(Plataforma_Exemplo, PosiçãoInicial, RotaçãoInicial, this.transform);
            PlataformaFlutuante.tag = "Plataforma";
            PlataformaFlutuante.GetComponent<Animator>().enabled = true;
        }
        if (PlataformaFlutuante != null & PlataformaFlutuante.GetComponent<FallingPlatform>().getTouchedBool())
        {
            Destroy(PlataformaFlutuante, 10);
            PlataformaFlutuante.GetComponent<Animator>().enabled = false;
        }

	}
}

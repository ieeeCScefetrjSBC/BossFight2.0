
using UnityEngine;

public class TiroPlayer : MonoBehaviour {

    private GameObject boss;
    private GameObject mascaraAzul;
    private GameObject mascaraVerde;
    private GameObject mascaraVermelho;
    public AudioSource SomTiro;
    public Camera mira;
    public float alcance =9200000000000000f;




    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        mascaraAzul = GameObject.FindGameObjectWithTag("Mascara1");
        mascaraVerde = GameObject.FindGameObjectWithTag("Mascara2");
        mascaraVermelho= GameObject.FindGameObjectWithTag("Mascara3");

    }



    void Update () {


        if (Input.GetButton("Fire1"))
        {
            Atira();
        }
		
	}

    void Atira()
    {
        SomTiro.Play();
        RaycastHit bang;

         if (Physics.Raycast(mira.transform.position, mira.transform.forward, out bang, alcance))
         {
            if(bang.transform.name == "ThunderBlue" || bang.transform.name == "Boss")
            {
				if(mascaraAzul!=null)
                	mascaraAzul.GetComponent<Vida_Mascara_1>().setVida(2f);
            }
			if(bang.transform.name == "ThunderGreen" || bang.transform.name == "Boss")
            {
				if(mascaraVerde!=null)
				mascaraVerde.GetComponent<Vida_Mascara_2>().setVida(2f);

            }
			if(bang.transform.name == "ThunderRed" || bang.transform.name == "Boss")
            {
				if(mascaraVermelho!=null)
				mascaraVermelho.GetComponent<Vida_Mascara_3>().setVida(2f);
            }
            /*GameObject tiro = (GameObject)Instantiate(Resources.Load("TiroPlayer"), transform.position, Quaternion.identity);
            tiro.transform.rotation = mira.transform.rotation;
            tiro.transform.position = mira.transform.position;
            tiro.GetComponent<Rigidbody>().velocity = tiro.transform.forward * 19;*/
            //Debug.Log(bang.transform.name);
        }
    }
}


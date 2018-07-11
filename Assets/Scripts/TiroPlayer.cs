
using UnityEngine;

public class TiroPlayer : MonoBehaviour
{
    public AudioSource SomTiro;
    public Camera mira;

    public float alcance      = 9200000000000000f;
    public float cooldownTime = 0.5f;
    public float laserTime    = 0.5f;

    private GameObject boss;
    private GameObject mascaraFogo;
    private GameObject mascaraTempestade;
    private GameObject mascaraAgua;
    private GameObject pontaDaArma;
    private LineRenderer laserLine;                      
    private Light shotLight;

    private float lastShotTime = 0f;
    private float timeSinceShot;

    private bool justShot = false;

    private void Start()
    {
        boss            = GameObject.FindGameObjectWithTag("Boss");
        mascaraFogo     = GameObject.FindGameObjectWithTag("Mascara1");
        mascaraTempestade    = GameObject.FindGameObjectWithTag("Mascara2");
        mascaraAgua = GameObject.FindGameObjectWithTag("Mascara3");

        pontaDaArma       = GameObject.Find("Ponta da Arma");
        laserLine         = GameObject.Find("Laser").GetComponent<LineRenderer>();
        shotLight         = gameObject.GetComponentInChildren<Light>();
        laserLine.enabled = false;
        shotLight.enabled = false;

        timeSinceShot = -cooldownTime;
    }

    void FixedUpdate () {

        timeSinceShot = Time.time - lastShotTime;

        if (justShot)
        {
            RenderLaser();

            if (timeSinceShot >= laserTime)
            {
                justShot = false;
                laserLine.enabled = false;
                shotLight.enabled = false;
            }
        }

        if (Input.GetButtonDown("Fire1") && timeSinceShot >= cooldownTime)
        {
            SomTiro.Play();
            Atira();
            
            lastShotTime = Time.time;
            justShot     = true;

            laserLine.enabled = true;
            shotLight.enabled = true;
        }
	}

    void RenderLaser()
    {
        Ray ray = new Ray(pontaDaArma.transform.position, pontaDaArma.transform.forward);
        RaycastHit Hit;

        laserLine.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out Hit, 100))
            laserLine.SetPosition(1, Hit.point);
        else
            laserLine.SetPosition(1, ray.GetPoint(100));
    }

    void Atira()
    {
        RaycastHit bang;

         if (Physics.Raycast(mira.transform.position, mira.transform.forward, out bang, alcance))
         {
            if(bang.transform.gameObject.name == "Fire" || bang.transform.gameObject.tag == "Mascara1")
            {
                if(mascaraFogo != null)
                mascaraFogo.GetComponent<Vida_Mascara_1>().setVida(2f);
            }

			if(bang.transform.gameObject.name == "ThunderMask" || bang.transform.gameObject.tag == "Mascara2")
            {
                if(mascaraTempestade != null)
				mascaraTempestade.GetComponent<Vida_Mascara_2>().setVida(2f);

            }

			if(bang.transform.name == "rain" || bang.transform.name == "Mascara3")
            {
                if(mascaraAgua != null)
				mascaraAgua.GetComponent<Vida_Mascara_3>().setVida(2f);
            }
            /*GameObject tiro = (GameObject)Instantiate(Resources.Load("TiroPlayer"), transform.position, Quaternion.identity);
            tiro.transform.rotation = mira.transform.rotation;
            tiro.transform.position = mira.transform.position;
            tiro.GetComponent<Rigidbody>().velocity = tiro.transform.forward * 19;*/
            //Debug.Log(bang.transform.name);
         }
    }
}


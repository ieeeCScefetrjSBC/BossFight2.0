
using UnityEngine;

public class TiroPlayer : MonoBehaviour {
    public AudioSource SomTiro;
    public Camera mira;
    public float alcance =9200000000000000f;
    public float cooldownTime = 0.5f;
    public float laserTime = 0.5f;

    private GameObject boss;
    private GameObject mascaraAzul;
    private GameObject mascaraVerde;
    private GameObject mascaraVermelho;
    private GameObject pontaDaArma;
    private LineRenderer laserLine;                      
    private Light shotLight;
    private float lastShotTime = 0f;
    private float timeSinceShot;
    private bool justShot = false;

    private Laser laserScript; // DEBUGAR
    public bool useLaserScript = false;

    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        mascaraAzul = GameObject.FindGameObjectWithTag("Mascara1");
        mascaraVerde = GameObject.FindGameObjectWithTag("Mascara2");
        mascaraVermelho = GameObject.FindGameObjectWithTag("Mascara3");

        timeSinceShot = -cooldownTime;

        pontaDaArma = GameObject.Find("Ponta da Arma");
        laserLine = GameObject.Find("Laser").GetComponent<LineRenderer>();
        shotLight = gameObject.GetComponentInChildren<Light>();
        laserLine.enabled = false;
        shotLight.enabled = false;

        /////////////////////
        //laserScript = gameObject.GetComponentInChildren<Laser>();
        laserScript = pontaDaArma.GetComponent<Laser>();
        if (laserScript == null)
            Debug.Log("LASER SCRIPT MISSING");
        else
            Debug.Log("LASER SCRIPT FOUND");
        /////////////////////
    }

    void FixedUpdate () {

        timeSinceShot = Time.time - lastShotTime;

        if (!useLaserScript)
        {
            if (justShot)
            {
                RenderLaser();

                if (timeSinceShot >= laserTime)
                {
                    laserLine.enabled = false;
                    shotLight.enabled = false;
                    justShot = false;
                }

            }
        }

        if (Input.GetButtonDown("Fire1") && timeSinceShot >= cooldownTime)
        {
            SomTiro.Play();
            Atira();
            
            lastShotTime = Time.time;

            if (!useLaserScript)
            {
                justShot = true;
                laserLine.enabled = true;
                shotLight.enabled = true;
            }
            ///////////////////////////
            else
            {
                Ray ray = new Ray(pontaDaArma.transform.position, pontaDaArma.transform.forward);
                RaycastHit Hit;
                Vector3 target;

                if (Physics.Raycast(ray, out Hit, alcance))
                    target = Hit.point;
                else
                    target = ray.GetPoint(alcance);

                laserScript.ShootLaser(pontaDaArma.transform.position, target);
            }
            
            ///////////////////////////

            
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

        //Line.SetPosition(1, ray.GetPoint(100));
    }

    void Atira()
    {
        
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    public AudioSource shotSound;
    public float coolDownTime = 0.1f;
    public float lightTime = 0.1f;

    private GameObject Ponta_Arma;
    private GameObject Tiro;
    private Camera MainCamera;
    private Light shotLight;

    private Vector3 Direction;
    private float lastShotTime = 0f;
    private float timeSinceShot;

	void Start ()
    {
        Ponta_Arma = GameObject.FindGameObjectWithTag("Arma");
        Tiro = GameObject.FindGameObjectWithTag("Tiro_Player");
        MainCamera = Camera.main;
        shotLight = Ponta_Arma.GetComponent<Light>();

        shotLight.enabled = false;
        timeSinceShot = -coolDownTime;
	}
	
	void Update ()
    {
        timeSinceShot = Time.time - lastShotTime;

        if (timeSinceShot >= lightTime)
            shotLight.enabled = false;

        if (Input.GetMouseButtonDown(0) && timeSinceShot >= coolDownTime)
        {
            lastShotTime = Time.time;
            shotLight.enabled = true;
            shotSound.Play();

            Atira();
        }

    }

    void Atira()
    {
        RaycastHit Hit;
        
        Debug.Log("OPA");
        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward))
        {
            GameObject Projetil = (GameObject)GameObject.Instantiate(Tiro, Ponta_Arma.transform.position + Ponta_Arma.transform.forward.normalized * 2,
                                   Quaternion.Euler(MainCamera.transform.rotation.eulerAngles + new Vector3(90,0,0)));
            Projetil.GetComponent<Tiro_Blaster>().enabled = true;
            Projetil.GetComponent<Tiro_Blaster>().set_Direction(MainCamera.transform.forward);
        }
    }
}

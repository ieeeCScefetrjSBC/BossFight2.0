using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    public AudioSource shotSound;
    public float coolDownTime = 0.1f;
    public float lightTime = 0.1f;
    public bool automaticFire = false;

    private GameObject Ponta_Arma;
    private GameObject Tiro;
    private Camera MainCamera;
    private Light shotLight;

    private Vector3 Direction;
    private float lastShotTime;

	void Start ()
    {
        Ponta_Arma = GameObject.FindGameObjectWithTag("Arma");
        Tiro = GameObject.FindGameObjectWithTag("Tiro_Player");
        MainCamera = Camera.main;
        shotLight = Ponta_Arma.GetComponent<Light>();

        shotLight.enabled = false;
        lastShotTime = -coolDownTime;
    }
	
	void Update ()
    {
        bool shotFired = false;
        float timeSinceShot = Time.time - lastShotTime;

        if (timeSinceShot >= lightTime)
            shotLight.enabled = false;

        if (automaticFire)
            shotFired = Input.GetMouseButton(0);
        else
            shotFired = Input.GetMouseButtonDown(0);

        if (shotFired && timeSinceShot >= coolDownTime)
        {
            lastShotTime = Time.time;
            shotLight.enabled = true;
            shotSound.Play();

            Shoot();
        }

    }

    void Shoot()
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

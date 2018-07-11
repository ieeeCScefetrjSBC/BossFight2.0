using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    //VARIÁVEIS PÚBLICAS
    public float laserLenght = 5f;
    public float laserSpeed = 5f;

    //VARIÁVEIS PRIVADAS
    private LineRenderer laserLine;
    private Light shotLight;

    private Vector3 laserDirection;
    private Vector3 laserOrigin;
    private Vector3 laserTarget;

    private float timeShot;
    private float travelDistance;
    private bool isActive = false;

    void Start()
    {
        laserLine = gameObject.GetComponent<LineRenderer>();
        shotLight = gameObject.GetComponent<Light>();

        //laserLine.enabled = false;
        //shotLight.enabled = false;
    }

    void Update()
    {
        if (isActive)
        {
            float distCovered = laserSpeed * (Time.time - timeShot);
            float fracJourney = distCovered / travelDistance;
            Vector3 journeyPivot = laserOrigin - laserDirection;

            float travelTime = travelDistance / laserSpeed;
            if ((Time.time - timeShot) >= travelTime)
                isActive = false;

            if ((Time.time - timeShot) >= 0.1)
                shotLight.enabled = false;

            if (fracJourney >= 1f)
                isActive = false;

            else
            {
                Vector3 laserBackEnd = Vector3.Lerp(journeyPivot, laserTarget, fracJourney);
                Vector3 laserFrontEnd = laserBackEnd + laserDirection;

                laserLine.SetPosition(0, laserBackEnd);
                laserLine.SetPosition(1, laserFrontEnd);
            }
        }

        else
        {
            laserLine.enabled = false;
            shotLight.enabled = false;
        }
    }

    public void ShootLaser(Vector3 gunTip, Vector3 hitPoint)
    {
        timeShot = Time.time;
        travelDistance = Vector3.Distance(gunTip, hitPoint);

        laserDirection = (hitPoint - gunTip).normalized * laserLenght;
        laserOrigin = gunTip;
        laserTarget = hitPoint;

        isActive = true;
        laserLine.enabled = true;
        shotLight.enabled = true;

        Debug.Log("LASER");
    }
}

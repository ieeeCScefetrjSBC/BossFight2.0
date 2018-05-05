using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer Line;
    Light Light;


    void Start()
    {

        Line = gameObject.GetComponent<LineRenderer>(); 
        Line.enabled = false; //sumir a linha do line renderer
        Light = gameObject.GetComponent<Light>();
        Light.enabled = false;
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1")) //comando para ativar laser com botão esquerdo do mouse, chama a corrotina
        {
            StopCoroutine("FireLaser");  //just in case
            StartCoroutine("FireLaser"); //just in case
        }
    }
    IEnumerator FireLaser() 
    {
        Line.enabled = true; //linha é ligada
        Light.enabled = true;

        while (Input.GetButton("Fire1")) //para disparos contínuos enquanto o botão está sendo apertado. Loopception
        {
			//Line.material.mainTextureOffSet = new Vector2 (0, Time.time); //muda a textura de acordo com o tempo
			Ray ray = new Ray(transform.position, transform.forward); //ray inicia na ponta da arma e vai para frene, alinhado com a arma
            RaycastHit Hit;

            Line.SetPosition(0, ray.origin); //começa no começo( ponta da arma)

            if (Physics.Raycast(ray, out Hit, 100))
                Line.SetPosition(1, Hit.point);
            else
                Line.SetPosition(1, ray.GetPoint(100));

            Line.SetPosition(1, ray.GetPoint(100)); //termina no final, ray 100 unidades adiante 
            
            yield return null; 
        }
        Line.enabled = false; //para a linha quando o botão deixa de ser apertado
        Light.enabled = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class OnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidiu");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Está colidindo");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Saiu da colisão");
    }
}
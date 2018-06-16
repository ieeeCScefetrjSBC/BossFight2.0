using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro_Blaster : MonoBehaviour {

    Vector3 Direction;
    [SerializeField] private float Tiro_Speed;

	void Start () {
        this.gameObject.GetComponent<Rigidbody>().velocity = Direction.normalized * Tiro_Speed;
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("Ativa particula");
    }

    public void set_Direction(Vector3 direction)
    {
        Direction = direction;
        return;
    }
}

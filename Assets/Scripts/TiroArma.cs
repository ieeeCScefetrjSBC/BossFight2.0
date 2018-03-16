
using UnityEngine;

public class TiroArma : MonoBehaviour {

    public float dano = 10f;
    public float range = 100f;

    public Camera fpscam;
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent< Animation > ().Play("Tiro");

            Atira();
        }
		
	}

    void Atira()
    {
        RaycastHit bang;
       
        if(Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out bang, range))
        {
            Debug.Log(bang.transform.name);
        }
    }
}

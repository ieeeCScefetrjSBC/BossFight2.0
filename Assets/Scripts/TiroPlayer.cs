
using UnityEngine;

public class TiroPlayer : MonoBehaviour {

    
    public AudioSource SomTiro;
    public Camera mira;
    public float alcance = 200f;




    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }



    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Atira();
        }
		
	}

    void Atira()
    {
        SomTiro.Play();
        RaycastHit bang;

        GameObject tiro = (GameObject)Instantiate(Resources.Load("TiroPlayer"), transform.position, Quaternion.identity);
        if (Physics.Raycast(mira.transform.position, mira.transform.forward, out bang, alcance))
        {
            tiro.transform.rotation = mira.transform.rotation; 
            tiro.GetComponent<Rigidbody>().velocity = tiro.transform.forward * 12;
            Debug.Log(bang.transform.name);
            
            
            
        }
    }
}


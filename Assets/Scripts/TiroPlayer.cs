
using UnityEngine;

public class TiroPlayer : MonoBehaviour {

    private GameObject boss;
    public AudioSource SomTiro;
    public Camera mira;
    public float alcance =9200000000000000f;




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

        
       /* if (Physics.Raycast(mira.transform.position, mira.transform.forward, out bang, alcance))
        {
            GameObject tiro = (GameObject)Instantiate(Resources.Load("TiroPlayer"), transform.position, Quaternion.identity);
            tiro.transform.rotation = mira.transform.rotation;
            tiro.transform.position = mira.transform.position;
            tiro.GetComponent<Rigidbody>().velocity = tiro.transform.forward * 19;
            Debug.Log(bang.transform.name);
            
            
            
        }*/
    }
}


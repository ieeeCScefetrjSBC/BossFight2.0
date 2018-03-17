
using UnityEngine;

public class TiroPlayer : MonoBehaviour {

    private GameObject boss;
    public AudioSource SomTiro;
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

        GameObject tiro = (GameObject)Instantiate(Resources.Load("TiroPlayer"), transform.position, Quaternion.identity);
        tiro.transform.rotation = this.transform.rotation;
        tiro.GetComponent<Rigidbody>().velocity = tiro.transform.forward * 12;
        SomTiro.Play();
    }
}

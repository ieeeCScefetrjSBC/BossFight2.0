
using UnityEngine;

public class TiroPlayer : MonoBehaviour {

    private GameObject boss;
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

        tiro.GetComponent<Rigidbody>().velocity = (boss.transform.position - tiro.transform.position);
    }
}

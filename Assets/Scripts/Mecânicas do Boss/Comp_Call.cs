using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp_Call : MonoBehaviour {

    // Use this for initialization
    private float Tempo = 5f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Tempo -= Time.deltaTime;
        if(Tempo<=0)
        {
            this.gameObject.GetComponent<Comp_Bomba>().Call(2);
        }
	}
}

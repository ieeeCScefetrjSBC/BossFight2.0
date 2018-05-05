using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFade : MonoBehaviour {
    //light fades as the bomb goes boom
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Light>().range = Mathf.Lerp(GetComponent<Light>().range, 0, Time.deltaTime); //goes from light to dark
	}
}

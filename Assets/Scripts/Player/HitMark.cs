using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMark : MonoBehaviour {

    private float Tempo;

	void Start () {
        Tempo = 0.6f;
	}
	
	void Update () {
        Tempo -= Time.deltaTime;
        if(Tempo <= 0)
        {
            Destroy(gameObject);
        }
	}
}

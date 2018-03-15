using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour {
    public float sensitivity = 2f;
    public GameObject eyes;

    float rotX;
    float rotY;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;

	}

    // Update is called once per frame
    void Update()
    {
        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0, rotX, 0);
        eyes.transform.Rotate(-rotY, 0, 0);


    }
}

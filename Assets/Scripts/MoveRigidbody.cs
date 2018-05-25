using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRigidbody : MonoBehaviour {
    
    public AudioSource moveAudio;
    public Animator playerAnim;
    public float sensitivity = 2f;

    private float rotX;
    private float rotY;

    private Rigidbody rigidbody;
    private Vector3 moveDirection = Vector3.zero;
    private float speed = 9.0F;
    private float jumpSpeed = 20F;
    private float gravity = 45.0F;
    private float inputV;
    private float inputH;
    
    private float vertSpeed = 0f;//velocidade vertical do player (precisa disso para poder mexer enquanto está no pulo)
    private bool platPulo = false;//vSariavel que detecta a colisão com a plataforma de pulo
    private bool invertedControl = false;

    private string groundTag;           //DEBUGAR
    private bool isGrounded = false;
    private bool isMoving = false;
    private bool wasMoving = false;
    private bool runButton = false;
    
    // Use this for initialization
    void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void checkGrounded()
    {
        //int groundLayerMask = 1 << 8;
        //isGrounded = Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), 1.5f, groundLayerMask);


    }

	void OnCollisionEnter (Collision collision)
	{
		string objectTag = collision.gameObject.tag;

		if (objectTag == "Ground" )
		
	}

    // Update is called once per frame
    void FixedUpdate ()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection.normalized;

        moveDirection *= speed;
    }
}

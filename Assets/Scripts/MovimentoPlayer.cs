using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float sensitivity = 2f;
	public AudioSource MoviSound;
    public Animator Player_Anim;
    //public GameObject eyes;


    float rotX;
    float rotY;

    private float speed = 9.0F;
    private float jumpSpeed = 20F;
    private float gravity = 45.0F;
    private float InputV;
    private float InputH;
    private Vector3 moveDirection = Vector3.zero;
    private float vertVel = 0f;//velocidade vertical do player (precisa disso para poder mexer enquanto está no pulo)
    private bool platPulo = false;//vSariavel que detecta a colisão com a plataforma de pulo
    private bool InverterControlesAtivado = false;

	private bool isMoving = false;
	private bool wasMoving = false;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        //Movimentação do Player
        CharacterController controller = GetComponent<CharacterController>();

		if (Input.GetKey(KeyCode.LeftShift))
            speed = 15.0f;
        else if (platPulo)
            speed = 20f;
        else
            speed = 9.0F;



        if (!InverterControlesAtivado)//se InverterControlesAtivado for false, a movimentação do player segue normal
        {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        else// se InverterControlesAtivado for true ela é invertida
        {
            moveDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

		//////////////////////////////
		if (moveDirection != Vector3.zero)
			isMoving = true;
		else
			isMoving = false;

		if (isMoving && !wasMoving)
			MoviSound.Play ();

		if (!isMoving && wasMoving)
			MoviSound.Stop ();

		wasMoving = isMoving;
        //////////////////////////////

        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump") || platPulo)//faz o pulo do player quando o botão de pular for apertado, ou ele tenha colidido com a plataforma de pulo
                vertVel = jumpSpeed;
            else
                vertVel = moveDirection.y;
            Debug.Log("Grounded");
        }
        else
            Debug.Log("NotGrounded");
        vertVel -= gravity * Time.deltaTime;
        moveDirection.y = vertVel;
        controller.Move(moveDirection * Time.deltaTime);
        //////////////////////////////
        // ANIMAÇÃO DO PLAYER
        InputH = Input.GetAxis("Horizontal");
        InputV = Input.GetAxis("Vertical");

        Player_Anim.SetFloat("InputH", InputH);
        Player_Anim.SetFloat("InputV", InputV);

    }

    void OnControllerColliderHit(ControllerColliderHit hit) {//detecta colisão do player com a plataforma de pulo
        if(hit.gameObject.tag == "PlataformaPulo")
        {
            Debug.Log("A");
            jumpSpeed = 60f;
            platPulo = true;
            StartCoroutine(TempoDepoisDaPlataformaPulo());
        }
    }

    public void SetInverterControlesAtivado(bool Inverter)//usado para settar o atributo InverterControlesAtivado
    {
        InverterControlesAtivado = Inverter;
    }

    IEnumerator TempoDepoisDaPlataformaPulo()//esse metodo serve para reiniciar os valores de pulo depois que o player passa da plataforma de pulo
    {
        yield return new WaitForSeconds(1);
        platPulo = false;
        jumpSpeed = 20f;
    }
}
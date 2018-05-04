using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float sensitivity = 2f;
    public GameObject eyes;


    float rotX;
    float rotY;

    [SerializeField]
    private float soproTime;
    [SerializeField]
    private float soproForce;
    private float soproCounter = 0;

    private float speed = 9.0F;
    private float jumpSpeed = 25.0F;
    private float gravity = 45.0F;
    private Vector3 moveDirection = Vector3.zero;
    private float vertVel = 0f;//velocidade vertical do player (precisa disso para poder mexer enquanto está no pulo)
    private bool platPulo = false;//vSariavel que detecta a colisão com a plataforma de pulo

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

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        if (controller.isGrounded)
        {
            if (Input.GetButton("Jump") || platPulo)//faz o pulo do player quando o botão de pular for apertado, ou ele tenha colidido com a plataforma de pulo
                vertVel = jumpSpeed;
            else
                vertVel = moveDirection.y;
        }
        vertVel -= gravity * Time.deltaTime;
        moveDirection.y = vertVel;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {//detecta colisão do player com a plataforma de pulo
        if(hit.gameObject.tag == "PlataformaPulo")
        {
            Debug.Log("A");
            jumpSpeed = 45f;
            platPulo = true;
            StartCoroutine(TempoDepoisDaPlataformaPulo());
        }
    }

    IEnumerator TempoDepoisDaPlataformaPulo()//esse metodo serve para reiniciar os valores de pulo depois que o player passa da plataforma de pulo
    {
        yield return new WaitForSeconds(1);
        platPulo = false;
        jumpSpeed = 25f;
    }

    public void Knockback(Vector3 direcao)
    {
        soproCounter = soproTime;
        while(soproCounter > 0)
            moveDirection = direcao * soproForce;
    }
    
    public void setMoveDirection(Vector3 move)
    {
        this.moveDirection = move;
    }
}
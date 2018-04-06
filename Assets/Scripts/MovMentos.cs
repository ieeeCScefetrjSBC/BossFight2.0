using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMentos : MonoBehaviour
{
    public float speed = 2f;
    public float sensitivity = 2f;


    public GameObject eyes;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    float velocidadeVertical;
    float gravidade;
    float forcaPulo;
    private Rigidbody rb;

    public float speed2 = 10.0F;
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;



        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float translation = Input.GetAxis("Vertical") * speed2;
        float straffe = Input.GetAxis("Horizontal") * speed2;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);



        moveFB = Input.GetAxis("Vertical");
        moveLR = Input.GetAxis("Horizontal");

        rotX = Input.GetAxis("Mouse X") * sensitivity;
        rotY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 movimento = new Vector3(moveLR, 0, moveFB);
        transform.Rotate(0, rotX, 0);
        eyes.transform.Rotate(-rotY, 0, 0);




    }
 }































/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMentos : MonoBehaviour
{

    // Use this for initialization
    private bool Left, Up, Down, Right; // Direções 2D
    private bool StopLeft, StopUp, StopDown, StopRight; // Anti-Direções 2D
    public Rigidbody RB; // Rigidbody do Objeto
    private float Force = 3f, Max = 10f; // Força de aceleração e Velocidade Máxima
    void Start()
    {
        RB = GetComponent<Rigidbody>(); // Obtém o Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) // True caso tenha pressionado a tecla A
        {
            Left = true;  // Direção 
            Right = false; // Impede força oposta 
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Down = true; // idem
            Up = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Up = true; // idem
            Down = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right = true; // idem
            Left = false;
        }
        if (Input.GetKeyUp(KeyCode.A)) // True caso solte a tecla A
        {
            Left = false;  // Desliga o movimento 
            StopLeft = true; // Liga o anti-movimento
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Down = false; // idem
            StopDown = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Up = false; // idem
            StopUp = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Right = false; // idem
            StopRight = true;
        }

    }
    private void FixedUpdate()
    {
        if (Up && Mathf.Abs(RB.velocity.x) <= Max) // Caso a magnitude em Y seja menor que o Máx
            RB.AddForce(-Force, 0, 0, ForceMode.VelocityChange); // Força positiva
        if (Left && Mathf.Abs(RB.velocity.z) <= Max) //  Caso a magnitude em X seja menor que o Máx
            RB.AddForce(0, 0, Force, ForceMode.VelocityChange); // Força Negativa
        if (Down && Mathf.Abs(RB.velocity.x) <= Max)// Caso a magnitude em Y seja menor que o Máx
            RB.AddForce(Force, 0, 0, ForceMode.VelocityChange);// Força negativa
        if (Right && Mathf.Abs(RB.velocity.z) <= Max) // Caso a magnitude em X seja menor que o Máx
            RB.AddForce(0, 0, Force, ForceMode.VelocityChange); // Força positiva

        if (StopUp || StopDown) // Caso tenha que parar o movimento em Y
            RB.AddForce(-RB.velocity.x, 0, 0, ForceMode.VelocityChange);
        if (StopLeft || StopRight) // Caso tenha que parar o movimento em X
            RB.AddForce(0, 0, -RB.velocity.z, ForceMode.VelocityChange);
        StopRight = false; // Impede de aplicar várias vezes a força contrária
        StopUp = false;
        StopDown = false;
        StopLeft = false;


    }
}*/

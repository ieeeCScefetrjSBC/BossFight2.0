/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMentos : MonoBehaviour
{
    public float speed = 7.0F;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float straffe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;

        transform.Translate(straffe, 0, translation);
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

    }


}*/































using System.Collections;
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
            RB.AddForce(0, 0, -Force, ForceMode.VelocityChange); // Força positiva

        if (StopUp || StopDown) // Caso tenha que parar o movimento em Y
            RB.AddForce(-RB.velocity.x, 0, 0, ForceMode.VelocityChange);
        if (StopLeft || StopRight) // Caso tenha que parar o movimento em X
            RB.AddForce(0, 0, -RB.velocity.z, ForceMode.VelocityChange);
        StopRight = false; // Impede de aplicar várias vezes a força contrária
        StopUp = false;
        StopDown = false;
        StopLeft = false;


    }
}

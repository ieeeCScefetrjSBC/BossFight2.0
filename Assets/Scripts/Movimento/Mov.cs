using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour {

    // Use this for initialization
    private bool Left, Up, Down, Right; // Direções 2D
    private bool StopLeft, StopUp, StopDown, StopRight; // Anti-Direções 2D
    public Rigidbody RB; // Rigidbody do Objeto
    private float Force = 0.5f, Max = 5f; // Força de aceleração e Velocidade Máxima
    private float Extra_X=0f, Extra_Y=0f, Extra_Z=0f;
    private float Final_Force_X, Final_Force_Y;
    void Start()
    {
        RB = GetComponent<Rigidbody>(); // Obtém o Rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        Final_Force_X = 0f;
        Final_Force_Y = 0f;
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
        Final_Force_X += Extra_X;
        Final_Force_Y += Extra_Y;
        if (Up)
            Final_Force_Y += Force;
        if (Down)
            Final_Force_Y -= Force;
        if (Left)
            Final_Force_X -= Force;
        if (Right)
            Final_Force_X += Force;
            RB.AddForce(RB.transform.forward*Final_Force_Y+RB.transform.right*Final_Force_X + new Vector3(Extra_X,Extra_Y,Extra_Z), ForceMode.VelocityChange);
        if (RB.velocity.x <= -Max)
            RB.velocity = new Vector3(-Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.x >= Max)
            RB.velocity = new Vector3(Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.z >= Max)
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, Max);
        if (RB.velocity.z <= -Max)
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -Max);
        if (StopUp || StopDown) // Caso tenha que parar o movimento em Y
            RB.AddForce(0, -RB.velocity.y, 0, ForceMode.VelocityChange);
        if (StopLeft || StopRight) // Caso tenha que parar o movimento em X
            RB.AddForce(-RB.velocity.x, 0, 0, ForceMode.VelocityChange);
        Debug.Log(RB.velocity);

        StopRight = false; // Impede de aplicar várias vezes a força contrária
        StopUp = false;
        StopDown = false;
        StopLeft = false;


    }
    public void setExtra_Y(float Extra)
    {
        this.Extra_Y = Extra;
    }
    public void setExtra_X(float Extra)
    {
        this.Extra_Y = Extra;
    }
}

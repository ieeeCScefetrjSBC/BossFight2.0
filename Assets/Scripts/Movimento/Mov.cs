using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour {

    // Use this for initialization
    private bool Left, Up, Down, Right; // Direções 2D
    private bool StopLeft, StopUp, StopDown, StopRight; // Anti-Direções 2D
    public Rigidbody RB; // Rigidbody do Objeto
    private float Force = 0.6f, Max = 9f; // Força de aceleração e Velocidade Máxima
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
        
        if (Up)// Caso tecla W tenha sido pressionada
            Final_Force_Y += Force;
        else if(StopUp)// Caso tecla W tenha sido solta
            Final_Force_Y -= Force;
        if (Down)// Caso tecla S tenha sido pressionada
            Final_Force_Y -= Force;
        else if (StopDown) //Caso tecla S tenha sido solta
            Final_Force_Y += Force;
        if (Left) // Caso tecla A tenha sido pressionada
            Final_Force_X -= Force;
        else if(StopLeft)// Caso tecla A tenha sido solta
            Final_Force_X += Force;
        if (Right)// Caso tecla D tenha sido pressionada
            Final_Force_X += Force;
        else if (StopRight)// Caso tecla D tenha sido solta
            Final_Force_X -= Force;
        //Somatório das forças no eixo "Frente/Trás", eixo "Esquerda/Direita" e forças Extras
        RB.AddForce(RB.transform.forward*Final_Force_Y+RB.transform.right*Final_Force_X + new Vector3(Extra_X,Extra_Y,Extra_Z), ForceMode.VelocityChange);
        if (RB.velocity.x <= -Max)// Velocidade máxima negativa em X
            RB.velocity = new Vector3(-Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.x >= Max)// Velocidade máxima positiva em X
            RB.velocity = new Vector3(Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.z >= Max)// Velocidade máxima positiva em Z
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, Max);
        if (RB.velocity.z <= -Max)// Velocidade máxima negativa em Z
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -Max);
        Debug.Log(RB.velocity);
       


        StopRight = false; // Impede de aplicar várias vezes a força contrária
        StopUp = false;
        StopDown = false;
        StopLeft = false;


    }
    public void setExtra_Y(float Extra)
    {
        this.Extra_Y += Extra;
    }
    public float getExtra_Y()
    {
        return this.Extra_Y;
    }
    public void setExtra_X(float Extra)
    {
        this.Extra_Y += Extra;
    }
    public void setExtra_Z(float Extra)
    {
        this.Extra_Z += Extra;
    }
}

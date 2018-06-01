using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour {

    // Use this for initialization
    private bool Left, Up, Down, Right; // Direções 2D
    private bool StopLeft, StopUp, StopDown, StopRight; // Anti-Direções 2D
    public Rigidbody RB; // Rigidbody do Objeto
    public Animator Player_Anim;
    [SerializeField] private float Force; //Força de aceleração
    [SerializeField] private float Max = 9f; // Velocidade Máxima
    [SerializeField] private float Impulso_PlatPulo;  // Impulso pra cima no player ao pisar na plat de pulo
    [SerializeField] private float JumpForce;
    private float Extra_X=0f, Extra_Y=0f, Extra_Z=0f;
    private float Final_Force_X, Final_Force_Y;
    private float ContadorDeTempo;
    private float Tempo_Recuperação;
    private float ValorParaRecuperar;
    private float InputV;
    private float InputH;
    private bool InverterControlesAtivado = false;
    public bool Grounded;  // Guarda a informação de se o player está no chão ou não
    private bool Congelado;
    private bool ActivateJump;
    private HeliceDeGelo HeliceDeGelo;

    void Start()
    {
        RB = GetComponent<Rigidbody>(); // Obtém o Rigidbody
        Cursor.lockState = CursorLockMode.Locked;
        ContadorDeTempo = 0;
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

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            ActivateJump = true;
        }

        // ANIMAÇÃO DO PLAYER
        InputH = Input.GetAxis("Horizontal");
        InputV = Input.GetAxis("Vertical");
        Player_Anim.SetFloat("InputH", InputH);
        Player_Anim.SetFloat("InputV", InputV);

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
        if(!InverterControlesAtivado)
           // RB.AddForce(RB.transform.forward*Final_Force_Y+RB.transform.right*Final_Force_X + new Vector3(Extra_X,Extra_Y,Extra_Z), ForceMode.VelocityChange);
            RB.AddForce(RB.transform.forward * Final_Force_Y  + RB.transform.right * Final_Force_X , ForceMode.Force);
        else
            RB.AddForce(-RB.transform.forward * Final_Force_Y - RB.transform.right * Final_Force_X + new Vector3(Extra_X, Extra_Y, Extra_Z), ForceMode.VelocityChange);

        /*if (RB.velocity.x <= -Max)// Velocidade máxima negativa em X
            RB.velocity = new Vector3(-Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.x >= Max)// Velocidade máxima positiva em X
            RB.velocity = new Vector3(Max, RB.velocity.y, RB.velocity.z);
        if (RB.velocity.z >= Max)// Velocidade máxima positiva em Z
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, Max);
        if (RB.velocity.z <= -Max)// Velocidade máxima negativa em Z
            RB.velocity = new Vector3(RB.velocity.x, RB.velocity.y, -Max); */

        /*Speed Limit */
        Vector3 VelHor = new Vector3(RB.velocity.x, 0, RB.velocity.z);
        if (VelHor.magnitude >= Max)
        {
            RB.velocity = (VelHor.normalized * Max) + new Vector3(0, RB.velocity.y, 0);
        }
       // Debug.Log(RB.velocity.magnitude);

        /* Jump Mechanic*/

        int layerMask = 1 << 8;
        Grounded = Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), 1.1f, layerMask);
        //Debug.Log(Grounded);
        if (ActivateJump == true)
        {
            RB.velocity += new Vector3 (0f, JumpForce, 0f);
            ActivateJump = false;
        }



        /*Recuperação do Congelamento*/
        if (Congelado)
        {
            ContadorDeTempo += Time.deltaTime;
            if(ContadorDeTempo >= Tempo_Recuperação)
            {
                Force += ValorParaRecuperar;
                Congelado = false;
            }
        }



        StopRight = false; // Impede de aplicar várias vezes a força contrária
        StopUp = false;
        StopDown = false;
        StopLeft = false;

    }
    //////////////////////////////////////////////////

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "PlataformaPulo")
        {
            RB.AddForce(Vector3.up * Impulso_PlatPulo, ForceMode.VelocityChange);
            Debug.Log("KARAAAAI VIADO");
        }
    }

    public void SetInverterControlesAtivado(bool Inverter) //usado para settar o atributo InverterControlesAtivado
    {
        InverterControlesAtivado = Inverter;
    }


    public void setForce_Congelamento(float Força_Congelamento)
    {
        this.Force -= Força_Congelamento;
    }

    public void setBool_Congelado(bool Congelado)
    {
        this.Congelado = Congelado;
    }

    public void setTempo_Recuperação(float tempo_Recuperação)
    {
        this.Tempo_Recuperação = tempo_Recuperação;
    }

    public void setValorParaRecuperar(float valor_recuperação)
    {
        this.ValorParaRecuperar = valor_recuperação;
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

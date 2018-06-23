using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliceDeGelo : MonoBehaviour
{

    public float partialFreezeDist = 15f;
    public float totalFreezeDist = 3.5f;

    private GameObject Player;
    private Vector3 Mov_Direção; //Vetor direção do movimento
    private Vector3 direction; // Vetor direção da força
    private MoveRigidbody moveScript;
    [SerializeField] private float Aceleração; // Aceleração 
    [SerializeField] private float Vel_Max;    // Velocidade máxima de movimento
    [SerializeField] private float Vel_Rot;    // Rotação da helice
    [SerializeField] private float Intens_CongelamentoParcial;  // Intensidade do congelamento parcial
    [SerializeField] private float Intens_CongelamentoTotal;  // Intensidade do congelamento total
    [SerializeField] private float DistanciaDeAtivação; // Distância em que o sopro da hélice é ativado
    [SerializeField] private float Dano_HeliceDeGelo;   // Dano causado pela helice de gelo
    [SerializeField] private float Tempo_de_Vida;       // Tempo para auto destruição
    public float TempoCongelado;   // Tempo que o player permanece congelado
    private bool Longe = true;
    private bool Encostou = false;
    private bool Ativar_CongelamentoParcial = false;      // Ativa ou desativa o congelamento parcial, diminuindo a velocidade do player
    private bool Ativar_CongelamentoTotal = false;        // Ativa ou desativa o congelamento total, paralisação do player
    private bool Alterou_Parcial = false;                 // Verifica se já aplicou o efeito parcial da tempestade ( antes de encostar )
    private bool Alterou_Total = false;                   // Verifica se já aplicou o efeito total da tempestade   ( depois de encostar )
    private bool Velocidade_Normal = true;                // Verifica se a velocidade está normal ou alterada
    private bool VelMax_Atingida = false;
    private Vida_Player Vida_Player;

    private float dist2Player = Mathf.Infinity;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Vida_Player = Player.GetComponent<Vida_Player>();
        moveScript = Player.GetComponent<MoveRigidbody>();
    }

    public float GetDistToPlayer()
    {
        return dist2Player;
    }

    void Update()
    {
        // TEMPO PARA AUTO DESTUIÇÃO !!!
        if (Tempo_de_Vida > 0)
        {
            Tempo_de_Vida -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // MOVIMENTO DA HELICE!!!
        Mov_Direção = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.5f, Player.transform.position.z) - transform.position;  //Direção de movimento da helice
        transform.Rotate(Vector3.up * Time.deltaTime * Vel_Rot, Space.World);   // rotação em y
        if ((Player.transform.position - transform.position).magnitude <= totalFreezeDist)     // Verifica se está perto ou distante do player
        {
            Longe = false;
            Encostou = true;
        }
        if (this.gameObject.GetComponent<Rigidbody>().velocity.magnitude >= Vel_Max)
        {
            VelMax_Atingida = true;
        }
        else
        {
            VelMax_Atingida = false;
        }

        dist2Player = (transform.position - Player.transform.position).magnitude;
        if (dist2Player < totalFreezeDist)
        {
            Vida_Player.danoPlayer(Dano_HeliceDeGelo);
            moveScript.SetFreezeState(FreezeStates.isFrozen);
            Destroy(gameObject);
        }
        else if (dist2Player < partialFreezeDist && moveScript.GetFreezeState() != FreezeStates.isFrozen)
        {
            moveScript.SetFreezeState(FreezeStates.isFreezing);
        }

        //if (Encostou)
        //{
        //    if (dist2Player == moveScript.GetDist2IceHelix())
        //        moveScript.SetDist2IceHelix(Mathf.Infinity);

        //    Vida_Player.danoPlayer(Dano_HeliceDeGelo);
        //    Destroy(gameObject);
        //}

        // CONGELAMENTO PARCIAL -- > DIMINUI VELOCIDADE DE MOVIMENTO
        //direction = transform.position - Player.transform.position;  // Direção do player à helice
        //if (direction.magnitude <= DistanciaDeAtivação)
        //{
        //    Ativar_CongelamentoParcial = true;
        //}
        //else
        //{
        //    Ativar_CongelamentoParcial = false;
        //}

        //if(Ativar_CongelamentoParcial == true && Velocidade_Normal == true)
        //{
        //    Alterou_Parcial = true;
        //    Velocidade_Normal = false;
        //    moveScript.setForce_Congelamento(Intens_CongelamentoParcial);                // Ativa o congelamento parcial caso entre no raio de ação
        //}
        //if(Ativar_CongelamentoParcial == false && Velocidade_Normal == false && Alterou_Total == false)
        //{
        //    Alterou_Parcial = false;
        //    Velocidade_Normal = true;
        //    moveScript.setForce_Congelamento(-Intens_CongelamentoParcial);              // Desativa o congelamento caso saia do raio de ação
        //}
        //if (Encostou)
        //{
        //    Destroy(this.gameObject);
        //    Player.gameObject.GetComponent<Vida_Player>().danoPlayer(Dano_HeliceDeGelo);
        //    Ativar_CongelamentoParcial = false;
        //    Alterou_Parcial = false;
        //    Ativar_CongelamentoTotal = true;
        //    Debug.Log("Atingiu");
        //    Encostou = false;
        //}
        //if(Ativar_CongelamentoTotal == true)
        //{
        //    Alterou_Total = true;
        //    moveScript.setForce_Congelamento(-Intens_CongelamentoParcial);  // Desativa o congelamento parcial para ficar apenas o efeito do congelamento total
        //    moveScript.setForce_Congelamento(Intens_CongelamentoTotal);     // Ativa o congelamento total caso a helice encoste com sucesso no player
        //    Ativar_CongelamentoTotal = false;
        //}
        //if(Alterou_Total == true)
        //{
        //    moveScript.setBool_Congelado(true);
        //    moveScript.setTempo_Recuperacao(TempoCongelado);
        //    moveScript.setValorParaRecuperar(Intens_CongelamentoTotal);
        //}
    }

    private void FixedUpdate()
    {
        if (Longe)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(Mov_Direção.normalized * Aceleração, ForceMode.VelocityChange);
            if (VelMax_Atingida)
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = Mov_Direção.normalized * Aceleração;
            }
        }
    }
}

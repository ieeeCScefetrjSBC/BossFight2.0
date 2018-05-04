using UnityEngine;

public class Tiro_Boss : MonoBehaviour {
    private float fireRate = 0.4f;                  //Quanto menor o fire rate mais tempo entre os tiros do boss 
    private float tempoAtirar = 1.5f; // Tempo entre cada tiro, impede o boss de atirar assim que inicia, esperando 1.5 segundos;
    private float Timer=2.5f; // Contador
    private string Pattern= "Sprayed_Shots";// Padrão, tem como início os tiros comuns
    private GameObject player; // Objeto Player
    public AudioSource AtqBoss;// Som do Tiro
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= tempoAtirar)
        {
            tempoAtirar = Time.time + 1f / fireRate; // Tempo entre cada tiro
            Atirar();// Chama a função que define qual tiro será realizado

        }


    }

    void Atirar ()
    {
        switch (Pattern) // Verifica qual o padrão
        {
            case "Regular_Shots": // Tiros lentos direcionados ao player
                GameObject tiro = (GameObject)Instantiate(Resources.Load("Tiro"), transform.position, Quaternion.identity);
                AtqBoss.Play();// Som de tiro
                break;
            case "Sprayed_Shots": // Tiros rápidos e imprecisos
                fireRate = 8f; // Velocidade de tiro ampliada
                GameObject tiro_spray = (GameObject)Instantiate(Resources.Load("Spray_Tiro"), transform.position, Quaternion.identity); // Instancia objeto
                Timer -= Time.deltaTime;// Decai contador
                
                if (Timer <= 0)// Caso a mecânica tenha acabado:
                {
                    Timer = 5f;// Reseta o cotador
                    fireRate = 0.4f;// Reseta a frequência de tiro
                    Pattern = "Regular_Shots";// Retorna para os tiros comuns
                }
                AtqBoss.Play();// Som de tiro
                break;
        }
        

        //tiro.GetComponent<Rigidbody>().velocity = (player.transform.position - tiro.transform.position);
       
        
    }
    public void setfireRate(float Add) // Define Velocidade de Tiro
    {
        this.fireRate = Add;
    }
    public float getfireRate() // Adquire a Velocidade de Tiro
    {
        return this.fireRate;
    }
    public void setPattern(string new_Pattern)// Define o padrão de tiro
    {
        this.Pattern = new_Pattern;
    }

}

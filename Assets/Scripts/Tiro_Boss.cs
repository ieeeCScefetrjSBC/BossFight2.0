using UnityEngine;

public class Tiro_Boss : MonoBehaviour {
    private float fireRate = 0.4f;                  //Quanto menor o fire rate mais tempo entre os tiros do boss 
    private float tempoAtirar = 0f;
    private string Pattern= "Sprayed_Shots";
    private GameObject player;
    public AudioSource AtqBoss;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        if (Time.time >= tempoAtirar)
        {
            tempoAtirar = Time.time + 1f / fireRate;
            Atirar();
        }

    }

    void Atirar ()
    {
        switch (Pattern) // Verifica qual o padrão
        {
            case "Regular_Shots": // Tiros lentos direcionados ao player
                GameObject tiro = (GameObject)Instantiate(Resources.Load("Tiro"), transform.position, Quaternion.identity);
                AtqBoss.Play();
                break;
            case "Sprayed_Shots": // Tiros rápidos e imprecisos
                fireRate = 8f;
                GameObject tiro_spray = (GameObject)Instantiate(Resources.Load("Spray_Tiro"), transform.position, Quaternion.identity);
                AtqBoss.Play();
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

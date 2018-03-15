using UnityEngine;

public class Tiro_Boss : MonoBehaviour {
    public float alcance = 100f;                   
    public float dano_tiro1 = 10f;                 //Tiro que não destroi a plataforma 
    public float dano_tiro2 = 20f;                 //Tiro mais forte que destroi a plataforma 
    public float fireRate = 100f;                  //Quanto menor o fire rate mais tempo entre os tiros do boss 
    public float tempoAtirar = 0f;
    public Camera fpsCam;                          
    public GameObject player;
    public Rigidbody tiroPrefab;
    private void Start()
    {
        GameObject tiro = (GameObject)Instantiate(Resources.Load("Tiro"));
        tiro.GetComponent<Rigidbody>().velocity = player.transform.position - tiro.transform.position;
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
        GameObject tiro = (GameObject)Instantiate(Resources.Load("Tiro"));
        tiro.GetComponent<Rigidbody>().velocity = player.transform.position - tiro.transform.position;
        Vector3 posicao = transform.position;//inicio do raycast
        Vector3 pos_player = posicao;//destino do raycast
        if (player)
        {
           pos_player  = player.transform.position; 
        }
        Vector3 direcao = pos_player - posicao;//direcao do raycast
        RaycastHit hit;
        if (Physics.Raycast(posicao, direcao, out hit, alcance)){//se o raycast bater em algo                                          
            Debug.Log(hit.transform.name);                       //registrar nome do objeto atingido
            if(hit.transform.name != "Player")                   
            {
                Destruir plataforma = hit.transform.GetComponent<Destruir>();
                if(plataforma != null)
                {
                    plataforma.Vida_Plataforma(dano_tiro2);
                }
            }
        }
    }
}

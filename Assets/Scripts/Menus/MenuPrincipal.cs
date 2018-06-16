using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    void Start()
    {

    }


    void Update()
    {

    }

    public void Jogar()
    {
        Debug.Log("Jogar");
        SceneManager.LoadScene("GD Room");
        Time.timeScale = 1;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        Debug.Log("Créditos");
    }

    public void Opcoes()
    {
        Debug.Log("Opções");
    }
}

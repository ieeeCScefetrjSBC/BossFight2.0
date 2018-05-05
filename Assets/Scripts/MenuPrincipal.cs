﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour {

    void Start()
    {
		Cursor.lockState = CursorLockMode.None;
    }


    void Update()
    {

    }

    public void Jogar()
    {
        Debug.Log("Jogar");
        SceneManager.LoadScene("GD Room");
    }

    public void Sair()
    {
        Debug.Log("Sair");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour {

	private Image tutorial;
	private GameObject menuPrincipal;
	private bool tutorialAtivado = false;

    void Start()
    {
    	tutorial = GameObject.FindGameObjectWithTag("tutorial").GetComponent<Image>();
    	tutorial.enabled = false;
    	menuPrincipal = GameObject.FindGameObjectWithTag("MenuPrincipal");
    	menuPrincipal.SetActive(true);
    }


    void Update()
    {
    	if(tutorialAtivado) 
    	{
    		if(Input.GetMouseButtonDown(0)) 
    		{
    			SceneManager.LoadScene("GD Room");
    			Time.timeScale = 1;
    		}
    	}
    }

    public void Jogar()
    {
        Debug.Log("Jogar");
        menuPrincipal.SetActive(false);
        tutorial.enabled = true;
        tutorialAtivado = true;
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        Debug.Log("Créditos");
        SceneManager.LoadScene("Creditos");
    }

    public void Opcoes()
    {
        Debug.Log("Opções");
    }
}

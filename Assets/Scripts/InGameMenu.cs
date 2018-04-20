using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour {
    private GameObject Player;
    private Camera MainCamera;
    private Canvas InGame_Menu;
    public Canvas Mira;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        InGame_Menu = Player.GetComponentInChildren<Canvas>();
        MainCamera = Player.GetComponentInChildren<Camera>();
        InGame_Menu.enabled = false;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            MainCamera.GetComponent<CamMove>().enabled = false;
            Player.GetComponent<TiroPlayer>().enabled = false;
            InGame_Menu.enabled = true;
            Mira.enabled = false;
        }
	}

    public void Continue()
    {
        InGame_Menu.enabled = false;
        Mira.enabled = true;
        Time.timeScale = 1;
        MainCamera.GetComponent<CamMove>().enabled = true;
        Player.GetComponent<TiroPlayer>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}

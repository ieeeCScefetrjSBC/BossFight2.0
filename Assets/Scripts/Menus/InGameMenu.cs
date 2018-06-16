using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    public Canvas inGameMenu;
    public Canvas mira;

    private GameObject Player;
    private CamMove    camMoveScript;
    private TiroPlayer tiroPlayerScript;
    private bool isPaused = false;

	void Start ()
    {
        Player           = GameObject.FindGameObjectWithTag("Player");
        camMoveScript    = Camera.main.GetComponent<CamMove>();
        tiroPlayerScript = Player.GetComponent<TiroPlayer>();

        inGameMenu.enabled = false;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            camMoveScript.enabled    = false;
            tiroPlayerScript.enabled = false;
            inGameMenu.enabled = true;
            mira.enabled       = false;
        }
	}

    public void Continue()
    {
        Time.timeScale = 1;

        camMoveScript.enabled    = true;
        tiroPlayerScript.enabled = true;

        inGameMenu.enabled = false;
        mira.enabled       = true;
        Cursor.lockState   = CursorLockMode.Locked;

        isPaused = false;
    }

    public void MainMenu()
    {
        if (isPaused)
        {
            Debug.Log("vaisefode2");
            SceneManager.LoadScene("Menu");
        }
    }
}

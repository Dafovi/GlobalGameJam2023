using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverCanvas;

    void Start()
    {
        pauseMenu.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    void Update(){

        gameOverCanvas.SetActive(GameManager.Instance.GameOver);
        Time.timeScale = Convert.ToInt32(!pauseMenu.activeSelf);

        if (!gameOverCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                pauseMenu.SetActive(!pauseMenu.activeSelf);

            Cursor.visible = pauseMenu.activeSelf;
        }
        else
        {
            Continue();
            Cursor.visible = gameOverCanvas.activeSelf;
        }
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
    }
}

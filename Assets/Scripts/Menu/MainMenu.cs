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
        Time.timeScale = GameManager.Instance.GameOver? 0 : Convert.ToInt32(!pauseMenu.activeSelf);

        if (!gameOverCanvas.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
        else Continue();
    }
    public void Continue()
    {
        pauseMenu.SetActive(false);
    }
}

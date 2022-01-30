using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public GameObject pausePanel;
    public GameObject instructions;

    public void Start()
    {
        paused = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pause or resume game depending on if the game is currently paused
            paused = !paused;
            if (paused)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Resume()
    {
        paused =! paused;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        instructions.SetActive(true);
    }

    public void InstructionsOff()
    {
        instructions.SetActive(false);
    }

}

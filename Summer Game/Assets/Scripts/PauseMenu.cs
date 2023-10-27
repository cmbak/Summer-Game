using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false; //static because it's used for the TYPE rather than an instance of an object? - clarify
    public GameObject pauseMenuUI;

    void Start()
    {
        Resume();
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           if (gameIsPaused)
           {
               Resume();
           }
           else
           {
               Pause();
           }
       }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.5f; //change to 0 after
        gameIsPaused = true;    
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("Main");
    }
    
    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit(); //Will this work for web gl? 
        //If not, create a quit scene
    }
}

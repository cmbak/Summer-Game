using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
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
               Resume();//Resume game
           }
           else
           {
               Pause();
               //Pause game
               //gameIsPaused = true;
           }
       }
    }

    private void Resume()
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenuUI;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        deathMenuUI.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Lives == 0 && player.HP == 0)
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        deathMenuUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//Chose current level from SceneManager
        Debug.Log("Restart level");
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

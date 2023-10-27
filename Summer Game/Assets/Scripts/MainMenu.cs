using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //public Random random = new Random();
    public TextMeshProUGUI splashText;
    public string[] splashPhrases = {"Reminder: It is on Stark Tower", "500", "Butt sky!", ">:O"};

    void Start()
    {
        splashText.SetText($"*{splashPhrases[Random.Range(0, splashPhrases.Length)]}*");
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting Game...");
        Application.Quit();
    }
}

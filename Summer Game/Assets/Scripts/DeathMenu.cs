using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Debug.Log("just die");
        }
    }

    private void ShowMenu()
    {
        deathMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Die");
    }
}

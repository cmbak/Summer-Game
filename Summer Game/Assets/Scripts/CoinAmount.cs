using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinAmount : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private PlayerController Player;
    private int amountOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        coinText = coinText.GetComponent<TextMeshProUGUI>(); //Looks for the TMPUGUI component in the coinText object
    }

    // Update is called once per frame
    void Update()
    {
      coinText.SetText($"x {Player.Coins.ToString()}");  
    }
}

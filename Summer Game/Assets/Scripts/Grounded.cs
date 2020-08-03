﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{   
    private GameObject Player;
    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
        playerScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            playerScript.isGrounded = true;
            playerScript.canDoubleJump = true;
            if (collision.collider.tag == "Ground") {playerScript.amountOfDoubleJumps ++;};
            playerScript.animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Platform")
        {
            playerScript.isGrounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "Respawn")
        {
            playerScript.Lives ++;

        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin")
        {
            //Debug.Log("Plyaer gound thing coollider iwth coin");
            //playerScript.Coins--;
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag != "PlayerGroundDetector")
        {
            if (collider.tag == "Coin")
            {
                playerScript.Coins --;
            }
        } 
    }*/
}

using System.Collections;
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
        if (collision.collider.tag == "Ground")
        {
            playerScript.isGrounded = true;
            playerScript.canDoubleJump = true;
            playerScript.amountOfDoubleJumps ++;
            playerScript.animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.collider.tag == "Ground")
        {
            playerScript.isGrounded = false;
        }
    }
}

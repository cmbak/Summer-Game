﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int amountOfDoubleJumps;
    public bool canDoubleJump;
    public float speed;
    public float jumpForce;
    public bool isGrounded = false;
    private Rigidbody2D rb;
    public Animator animator;
    public Vector3 respawnPoint;
    public int Lives;
    public int HP;
    public int maxHP = 100;
    public int Coins;
    public HealthBar healthBar;
    private int i = 0; //Used to verify coin triggering

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        isGrounded = true; 
        canDoubleJump = true;
        amountOfDoubleJumps = 1;
        
        Lives = 3;
        HP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfDoubleJumps > 3){amountOfDoubleJumps = 3;}
        
        
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += horizontal * Time.deltaTime * speed;

        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        Vector3 characterScale = transform.localScale;
        if(Input.GetAxis("Horizontal") > 0) //Moving right
        {
            characterScale.x = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0) //Moving left
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;

        /*if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        } 
        else if (Input.GetButtonDown("Jump") && isGrounded == false && canDoubleJump == true && amountOfDoubleJumps > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            amountOfDoubleJumps --;
            canDoubleJump = false;
            
        }*/
    }

    //Respawn
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Respawn")  //change to switch statements if necessary
        {
            transform.position = respawnPoint;
            Lives --;
            HP = maxHP;
            healthBar.SetHealth(maxHP);
            Debug.Log("Respawn");
        }
        else if (collider.tag == "Coin")
        {
            //if(i == 0)
            //{
                Debug.Log("Player picked up coin!");
                Coins ++;
                Destroy(collider.gameObject);
                i++;
            //}
            
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Coin")
        {
            if (i == 1)
            {
                i = 0;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Blobby") //Could change to layer mask with name of enemy in future - for different types of enemies
        {
            TakeDamage(10);
        }

        if (collision.collider.tag == "PlayerGroundDetector")
        {
            Debug.Log("Collided with child");
        }
        
    }

    private void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.SetHealth(HP);
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, );
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag =)
    }*/
}
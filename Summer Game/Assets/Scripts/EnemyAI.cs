﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D rigid2d;
    public float moveSpeed;
    public float actualSpeed;

    public float jumpForce;
    public float attackRange;
    public bool facingRight;
    public Transform castPoint;
    public Transform groundDetector;
    public bool isAggro;
    private bool isSearching;
    
    private Vector3 enemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        enemyTransform = transform.localScale;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {  
        
        if (CanSeePlayer(attackRange))
        {
            isAggro = true;        
        }
        else
        {
            if(isAggro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("stopChasingPlayer",3);
                }
            }
        }

        if(isAggro)
        {
            chasePlayer();
        }
        else{
            patrol();
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool seesPlayer = false;
        float raycastDistance = distance;

        if (facingRight == false) //if facing left
        {
            raycastDistance = -distance; //reverse direction of raycast
        }

        Vector2 endPos = castPoint.position + Vector3.right * raycastDistance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action")); //Layer Masking being what colliders to detect on the 'Action' Layer
        
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                //Aggravate enemy
                seesPlayer = true;
            }
            else
            {
                seesPlayer = false;
            }
        }
        return seesPlayer;
    }
     
    void chasePlayer()
    {
        if(Vector2.Distance(transform.position, Player.transform.position) < 2)
        {
            attackJump();
        }
        if (transform.position.x > Player.transform.position.x) //Player is on the left of enemy therefore turn left
        {
            facingRight = false;
            enemyTransform.x = -1;
            transform.localScale = enemyTransform;
            transform.Translate(-actualSpeed * Time.deltaTime * moveSpeed, 0, 0);   
        }
        else if (transform.position.x < Player.transform.position.x) //Player is on the right of enemy therefore turn right
        {
            facingRight = true;
            enemyTransform.x = 1;
            transform.localScale = enemyTransform;
            transform.Translate(actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
            
        }
    }

    void stopChasingPlayer()
    {
        isAggro = false;
        isSearching = false;
        patrol();
    }

    void move()
    {
        if (facingRight) 
        {
            enemyTransform.x = 1;
            transform.localScale = enemyTransform;
            transform.Translate(actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
        } 
        else if (!facingRight)
        {
            enemyTransform.x = -1;
            transform.localScale = enemyTransform;
            transform.Translate(-actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
        }  
    }

    void patrol()
    {
        isAggro = false;
        isSearching = false;

        RaycastHit2D groundDetection = Physics2D.Raycast(groundDetector.position, Vector2.down, 5f);
        if (groundDetection.collider != null) {
            move();
        }
        else if (groundDetection.collider == null)
        {
            if (facingRight)
            {
                facingRight = false;
                move();
            }    
            else if (!facingRight)
            {
                facingRight = true;
                move();
            }
        }  
    }

    void attackJump()
    {
        //rigid2d.AddForce(new Vector2 (0f, jumpForce), ForceMode2D.Impulse);
        rigid2d.velocity = Vector2.up * jumpForce;
    }
}   

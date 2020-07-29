using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D rigid2d; //CS0649 error comes up if using private - research and see if change is necessary
    public float moveSpeed;
    public float actualSpeed;
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
        if (isAggro)
        {
            chasePlayer();
        } else {
            patrol();
        }

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
                    Invoke("stopChasingPlayer", 3); //see if more efficient way of delaying this

                }
            }
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
        if (transform.position.x > Player.transform.position.x) //Enemy is to the right of the player
        {
            enemyTransform.x = -1;
            transform.Translate(-actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
            facingRight = false;
        }
        else if (transform.position.x < Player.transform.position.x) //Enemy is to the left of the player
        {
            enemyTransform.x = 1;
            transform.Translate(actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
            facingRight = true;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D rigid2d; //CS0649 error comes up if using private - research and see if change is necessary
    public float moveSpeed;
    public float attackRange;
    public bool facingRight;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {  
        Vector3 enemyTransform = transform.localScale;
        if (facingRight == true) 
        {
            enemyTransform.x = 1;
            transform.localScale = enemyTransform;
        } else if (facingRight == false)
        {
            enemyTransform.x = -1;
            transform.localScale = enemyTransform;
        }


        float distanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);
        //print(distanceToPlayer);     

        if (distanceToPlayer < attackRange) 
        {
            // Chase player
            chasePlayer();
        }  
        else 
        {
            //stop chasing player
            rigid2d.velocity = new Vector2(0, 0);
        }
    }

    void chasePlayer()
    {
        if (transform.position.x > Player.transform.position.x) //Enemy is to the right of the player
        {
            rigid2d.velocity = new Vector2(-moveSpeed, 0);
            facingRight = false;
        }
        else if (transform.position.x < Player.transform.position.x) //Enemy is to the left of the player
        {
            rigid2d.velocity = new Vector2(moveSpeed, 0);
            facingRight = true;

        }
    }
}

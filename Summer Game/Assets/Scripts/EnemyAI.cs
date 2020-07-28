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

    bool CanSeePlayer(float distance)
    {
        
        float raycastDistance = distance;
        Vector2 endPos = castPoint.position + Vector3.right * raycastDistance;
        RaycastHit2D hit = Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action")); //Layer Masking being what colliders to detect on the 'Action' Layer
        
        return true;
    }
     
    void chasePlayer()
    {
        if (transform.position.x > Player.transform.position.x) //Enemy is to the right of the player
        {
            transform.Translate(-actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
            facingRight = false;
        }
        else if (transform.position.x < Player.transform.position.x) //Enemy is to the left of the player
        {
            transform.Translate(actualSpeed * Time.deltaTime * moveSpeed, 0, 0);
            facingRight = true;
        }
    }
}

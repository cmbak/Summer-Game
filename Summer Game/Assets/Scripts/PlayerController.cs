using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int amountOfDoubleJumps;
    public bool canDoubleJump; //change to private after
    public float speed;
    public int amountOfCoins;
    public float jumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isGrounded = true; 
        canDoubleJump = true;
        amountOfDoubleJumps = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += horizontal * Time.deltaTime * speed;

        Vector3 characterScale = transform.localScale;
        if(Input.GetAxis("Horizontal") > 0) //If moving right
        {
            characterScale.x = 1;
        }
        else if (Input.GetAxis("Horizontal") < 0) //Moving left
        {
            characterScale.x = -1;
        }

        transform.localScale = characterScale;

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        } 
        else if (Input.GetButtonDown("Jump") && isGrounded == false && canDoubleJump == true && amountOfDoubleJumps > 0) //simplify /split conditionals into multiple if statements
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            amountOfDoubleJumps --;
            canDoubleJump = false;
            
        }
    }
}

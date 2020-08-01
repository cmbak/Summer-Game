using System.Collections;
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
    public int Coins;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        isGrounded = true; 
        canDoubleJump = true;
        amountOfDoubleJumps = 1;
        
        Lives = 3;
        HP = 100;
        Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetButtonDown("Jump") && isGrounded == true)
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
            
        }
    }

    //Respawn
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Respawn")  //change to switch statements if necessary
        {
            transform.position = respawnPoint;
            Lives --;
            Debug.Log("Respawn");
        }
        else if (collider.tag == "Coin")
        {
            Coins ++;
            Destroy(collider.gameObject);
        }

    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Blobby") //Could change to layer mask with name of enemy in future - for different types of enemies
        {
            HP -= 10;
        }
    }
}

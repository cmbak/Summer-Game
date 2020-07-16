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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true; 
        canDoubleJump = true;
        amountOfDoubleJumps = 1;
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
}

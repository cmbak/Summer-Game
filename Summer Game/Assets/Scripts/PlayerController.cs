using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Stats")]
    public int Lives;
    public int HP;
    public int maxHP = 100;
    public int Coins;
    [SerializeField]
    private int amountOfDoubleJumps;
    private int extraJumps;
    private Rigidbody2D rb;
    public HealthBar healthBar;
    public Animator animator;
    public Vector3 respawnPoint;
    [Header("Debugging")]
    [SerializeField]
    private bool canDoubleJump;
    [SerializeField]
    private bool playerGrounded;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

        isGrounded(); 
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
        if(isGrounded()){extraJumps = amountOfDoubleJumps;}
        
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

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded() && canDoubleJump && amountOfDoubleJumps > 0)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
            canDoubleJump = false;
        }
    }

    //Respawn
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Respawn")  //change to switch statements if necessary
        {
            transform.position = respawnPoint;
            Lives --;
            HP = maxHP;
            healthBar.SetHealth(maxHP);
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
            TakeDamage(10);
        }
    }

    private void TakeDamage(int damage)
    {
        HP -= damage;
        healthBar.SetHealth(HP);
    }

    private bool isGrounded() //rename isGrounded after
    {
        float distance = 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            canDoubleJump = true;
            playerGrounded = true;
            animator.SetBool("isJumping", false);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);
            animator.SetBool("isJumping", true);
            playerGrounded = false;
            return false;
        }
    }
}
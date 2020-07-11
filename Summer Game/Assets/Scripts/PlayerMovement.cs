using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    //public LayerMask groundLayer;
    //public RigidBody rb;
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
         sprite = GetComponent<SpriteRenderer>();
         isGrounded = true; //change if needed
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
    }
}

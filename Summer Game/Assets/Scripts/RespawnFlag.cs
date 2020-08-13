using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFlag : MonoBehaviour
{
    [SerializeField] private Animator animator;

    //Could verify input of string
    public string colour;
    //or switch statments using booleans
    //[SerializeField] pri
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
      colour = "Blue";  
    }

    // Update is called once per frame
    void Update()
    {
        //Colour verification
        switch (colour.ToLower())
        {
            case "red":
                animator.SetBool("isRed", true);
                animator.SetBool("isBlue", false);
                animator.SetBool("isGreen", false);
                animator.SetBool("isYellow", false);
                break;

            case "blue":
                animator.SetBool("isBlue", true);
                animator.SetBool("isRed", false);
                animator.SetBool("isGreen", false);
                animator.SetBool("isYellow", false);
                break;

            case "yellow":
                animator.SetBool("isYellow", true);
                animator.SetBool("isRed", false);
                animator.SetBool("isBlue", false);
                animator.SetBool("isGreen", false);
                break;

            case "green":
                animator.SetBool("isGreen", true);
                animator.SetBool("isRed", false);
                animator.SetBool("isYellow", false);
                animator.SetBool("isBlue", false);
                break;

            default:
                colour = "Blue";
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float offsetX = 2;
    private Transform playerTransform; //player movement
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {
        //Current position of camera - can't modify position of camera directly
        Vector3 tempPosition = transform.position;  
        //Set camera's x to the x of the player
        tempPosition.x = playerTransform.position.x + offsetX;  

        transform.position = tempPosition;
    }
}

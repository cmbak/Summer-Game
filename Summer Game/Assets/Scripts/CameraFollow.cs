using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float smoothTimeX = 5;
    public float smoothTimeY = 5;
    private Transform playerTransform; //player movement
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void LateUpdate()
    {

        float smoothPosX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref velocity.x, smoothTimeX); //passes the current velocity of the camera by ref
        float smoothPosY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);
    
    }
    
}

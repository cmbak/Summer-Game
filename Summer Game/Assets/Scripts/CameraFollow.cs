using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector2 velocity;
    public float smoothTimeX;
    public float smoothTimeY;

    public Transform zoomTarget;
    public bool wantBounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    private Transform playerTransform; //player movement
    private PlayerController player;
    
    [Header("Camera Movement")]
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private bool trackPlayer;
    [SerializeField] private float zoomTimeSpeed;
    
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        initialPosition = transform.position;
        trackPlayer = true;
        smoothTimeX = 0.2f;
        smoothTimeY = 0.4f;
        zoomTimeSpeed = 1.5f;
    }
    
    void Update() {

        if (player.Lives == 0 && player.HP == 0) {
            //Player dead
            Debug.Log("Player dead");
            Time.timeScale = 0.5f;
            trackPlayer = false;
            zoomOut();
        }
    }

    void FixedUpdate()
    {   
        if (wantBounds){
            transform.position = new Vector3( (Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x)),
            (Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y)),
            (Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)));
        }
    }

    void LateUpdate()
    {
        if (trackPlayer) {
            float smoothPosX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x, ref velocity.x, smoothTimeX); //passes the current velocity of the camera by ref
            float smoothPosY = Mathf.SmoothDamp(transform.position.y, playerTransform.position.y, ref velocity.y, smoothTimeY);

            transform.position = new Vector3(smoothPosX, smoothPosY, transform.position.z);
        }
    }

    private void zoomOut() {
        float zoomPosX = Mathf.SmoothDamp(transform.position.x, zoomTarget.position.x, ref velocity.x, smoothTimeX * zoomTimeSpeed);
        float zoomPosY = Mathf.SmoothDamp(transform.position.y, zoomTarget.position.y, ref velocity.y, smoothTimeY * zoomTimeSpeed);
        
        transform.position = new Vector3(zoomPosX, zoomPosY, transform.position.z);
    }
    
}

using System;
using UnityEngine;

/**
 * This component moves its object when the player clicks the arrow keys.
 */
public class KeyboardMover: MonoBehaviour {
    
    [SerializeField] float JummpVolocity = 10f;
    [SerializeField] private LayerMask platformLayer; //the platform that player can jump on it
    [SerializeField] float acceleration;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float accelerationRate;
    [SerializeField] float smallJump = 1.2f;
    [SerializeField] float bigJump = 1.75f;
    [SerializeField] float limitToBigJump = 17f;
    [SerializeField] float limitToSmallJump = 10f;
    private EdgeCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    private float minSpeed;
    private float minJummpVolocity;
    private bool canJump;
    public AudioSource normalJumpBeep;
    public AudioSource smallJumpBeep;
    public AudioSource bigJumpBeep;
    
    private void Awake()
    {
        boxCollider2D = transform.GetComponent<EdgeCollider2D>();
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        minSpeed = acceleration;
        minJummpVolocity = JummpVolocity;
        canJump = false;
    }

    private void Update()
    {
        //for fixed jumping 
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = true;
        }
        //if the player change is direction 
        if (Input.GetAxis("Horizontal") == 0) { 
            acceleration = minSpeed;
        }
    }

    void FixedUpdate() { 
        float horizontal = Input.GetAxis("Horizontal"); // +1 if right arrow is pushed, -1 if left arrow is pushed, 0 otherwise
        transform.position +=  new Vector3(horizontal*acceleration* Time.deltaTime, 0, 0);
        //add acceleration to player
        if (acceleration < maxSpeed&&Input.GetAxis("Horizontal")!=0) {
            acceleration += accelerationRate;
        }
        if (canJump)
        {
            canJump = false;
            //big jamp
            if (acceleration>limitToBigJump) {
                bigJumpBeep.Play();
                JummpVolocity *= bigJump;
            //small jump
            }else if (acceleration > limitToSmallJump)
            {
                smallJumpBeep.Play();
                JummpVolocity *= smallJump;
            }
            else
            {
                normalJumpBeep.Play();
            }
            //physical jump
            rigidbody2D.velocity = Vector2.up * JummpVolocity;
        }
        //rest speed
        if(acceleration<=minJummpVolocity)
        {
            JummpVolocity = minJummpVolocity;
        }
    }
    
    //this video helped me https://www.youtube.com/watch?v=ptvK4Fp5vRY
    private bool isGrounded()
    {
        RaycastHit2D RH = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down,1f, platformLayer);
        return RH.collider != null;
    }
    
    
}

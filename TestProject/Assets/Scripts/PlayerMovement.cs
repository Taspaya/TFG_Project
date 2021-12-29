using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Animator myAnimator;
    Rigidbody myRb;
    float horizontal;
    bool isGrounded = true;


    //Gravity implementation
    float gravity = -9.8f;
    float groundedGravity = -0.5f;
    [Header(" ======= Gravity Settings =========")]
    [SerializeField]
    [Tooltip("Gravity multiplier")]
    float gravityScale = 1;
    //Jumping variables
    bool isJumping = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1;
    float maxJumpTime = 0.5f;
    float jumpTimeCounter;

    [Header(" ======= Run Settings =========")]
    [Tooltip("Player Run Speed")]
    [SerializeField]
    float speed = 2;
    [Header(" ======= Jump Settings =========")]
    [Tooltip("Player Jump Settings")]
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    [Tooltip("Radius of the isGrouded collider checker")]
    float checkRadius = 1;
    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    float jumpForce = 3;

    [Header(" ======= Other Settings =========")]
    [SerializeField]
    [Tooltip("How long the player can keep going upwards when jumping")]
    float jumpTime = 2;


    [SerializeField]
    [Tooltip("Used to flip the mesh")]
    GameObject playerMesh;
    
    private void Awake()
    {
        myAnimator = playerMesh.GetComponent<Animator>();
        myRb = GetComponent<Rigidbody>();
        myRb.useGravity = false;
        SetupJumpVariables();
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayerMovement();
        FlipPlayer();
        ManageAnimations();
        ManageJump();
    }

    private void FixedUpdate()
    {
        //Just for Jump Events
        HandleGravity();
    }
    void ManagePlayerMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        position.x = position.x + 0.1f * horizontal * Time.deltaTime * speed * 10;
        transform.position = position;
    }

    void ManageJump()
    {
        //Jump
        isGrounded = Physics.OverlapSphere(feetPos.position, checkRadius, groundMask).Length > 0;


        if (!isJumping && isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            myRb.AddForce(Vector3.up * initialJumpVelocity * jumpForce);
        }
        else if (isJumping && isGrounded && !Input.GetButtonDown("Jump")) isJumping = false; 
        

        if(jumpTimeCounter > 0 && Input.GetButton("Jump"))
        {
            Debug.Log(jumpTimeCounter);
            jumpTimeCounter -= Time.deltaTime;
            myRb.AddForce(Vector3.up * initialJumpVelocity * jumpForce * 0.005f);
        }

        if ((jumpTimeCounter < 0 && isGrounded) || Input.GetButtonUp("Jump")) jumpTimeCounter = maxJumpTime;
    }

    void FlipPlayer()
    {
        if (horizontal > 0 && transform.rotation.y != 90) playerMesh.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        else if (horizontal < 0 && transform.rotation.y != -90) playerMesh.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
    }

    void ManageAnimations()
    {
        myAnimator.SetBool("isRunning", horizontal != 0);
        myAnimator.SetBool("isGrounded", isGrounded);
     }

    void HandleGravity()
    {
        if (isGrounded)
        {
            Vector3 gravityForce = groundedGravity * gravityScale * Vector3.up;
            myRb.AddForce(gravityForce, ForceMode.Acceleration);
        }
        else
        {
            Vector3 gravityForce = gravity * gravityScale * Vector3.up;
            myRb.AddForce(gravityForce, ForceMode.Acceleration);
        }
    }

    void SetupJumpVariables()
    {
        jumpTimeCounter = maxJumpTime;
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}

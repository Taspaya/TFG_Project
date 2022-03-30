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
    [Header(" ======= Gravity Settings =========")]
    const float FALL_SPEED = -10;

    //Jumping variables
    bool isJumping = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1;
    float currentJumpTime;
    float jumpTimeCounter;

    [Header(" ======= Run Settings =========")]
    [Tooltip("Player Run Speed")]
    [SerializeField]
    float speed = 2;
    float currentSpeed;
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
    float jumpTime = 0.5f;


    [SerializeField]
    [Tooltip("Used to flip the mesh")]
    GameObject playerMesh;
    private bool jumping;

    private void Awake()
    {
        myAnimator = playerMesh.GetComponent<Animator>();
        myRb = GetComponent<Rigidbody>();
        currentSpeed = speed;
        currentJumpTime = jumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.OverlapSphere(feetPos.position, checkRadius, groundMask).Length > 0 || (myRb.velocity.y < 0.1f && myRb.velocity.y > -0.1f);

        FlipPlayer();
        ManageAnimations();
        ManageWallJump();
        jumping = Input.GetButton("Jump");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            myRb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }
        else if (isGrounded && !Input.GetButtonDown("Jump")) isJumping = false;

        if(!isGrounded && Input.GetButton("Jump") && currentJumpTime > 0)
        {
            myRb.AddForce(transform.up * 0.5f, ForceMode.VelocityChange);
        }

        if (isGrounded && currentJumpTime <= 0) currentJumpTime = jumpTime;
        HandleGravity();
    }

    private void FixedUpdate()
    {
        if (!isGrounded && Input.GetButton("Jump") && currentJumpTime > 0)
        {
            currentJumpTime -= 0.1f;
        }

            ManagePlayerMovement();
        if (!isGrounded && myRb.velocity.y <= 0 && 
            !PlayerController.Instance.GetIsLeftLimited() && 
            !PlayerController.Instance.GetIsRightLimited())
            
            myRb.AddForce(-transform.up * (jumpForce * 0.1f ), ForceMode.Acceleration);
    }


    void ManageWallJump()
    {
        if(PlayerController.Instance.GetIsLeftLimited() && !isGrounded && Input.GetButtonDown("Jump"))
        {
            Vector3 walljumpForce = new Vector3(jumpForce, -myRb.velocity.y + jumpForce * 2, 0);
            myRb.AddForce(walljumpForce, ForceMode.Impulse);
            FlipPlayer();
        }
    }

    void ManagePlayerMovement()
    {
        if (!isGrounded) currentSpeed = speed / 2;
        else currentSpeed = speed;

        if (PlayerController.Instance.GetCanWalk()) horizontal = Input.GetAxis("Horizontal");
        else
        {
            horizontal = 0;
            myRb.velocity = new Vector3(0, myRb.velocity.y, 0);
        }

        if (!PlayerController.Instance.GetIsLeftLimited() && !PlayerController.Instance.GetIsRightLimited())
            myRb.velocity = myRb.velocity + new Vector3(horizontal * currentSpeed, 0, 0);
        else if (PlayerController.Instance.GetIsLeftLimited() && horizontal > 0)
            myRb.velocity = myRb.velocity + new Vector3(horizontal * currentSpeed, 0, 0);
        else if (PlayerController.Instance.GetIsRightLimited() && horizontal < 0)
            myRb.velocity = myRb.velocity + new Vector3(horizontal * currentSpeed, 0, 0);

    }


    void FlipPlayer()
    {
        if (PlayerController.Instance.GetCanWalk())
        {
            if (horizontal > 0 && transform.rotation.y != 90) playerMesh.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            else if (horizontal < 0 && transform.rotation.y != -90) playerMesh.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
    }

    void ManageAnimations()
    {
        myAnimator.SetBool("isRunning", ((horizontal < -0.5f || horizontal > 0.5f || horizontal != 0)));
        myAnimator.SetBool("isGrounded", isGrounded);
    }

    void HandleGravity()
    {
        if (!isGrounded && (myRb.velocity.y > FALL_SPEED && myRb.velocity.y < 0)) myRb.AddForce(-Vector3.up, ForceMode.VelocityChange);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
}

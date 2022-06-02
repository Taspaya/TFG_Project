using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_JumpManager : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 1;

    [SerializeField]
    Transform isGroundedTrans;

    [SerializeField]
    LayerMask layer;

    private bool isGrounded;
    private float horizontal;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.OverlapSphere(isGroundedTrans.position, 0.1f, layer).Length > 0;


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            transform.GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        if (!isGrounded && Input.GetKey(KeyCode.Space)) transform.GetComponent<Rigidbody>().AddForce(transform.up * (jumpForce / 100), ForceMode.VelocityChange);

        if (!isGrounded && !Input.GetKey(KeyCode.Space)) transform.GetComponent<Rigidbody>().AddForce(-transform.up * (jumpForce / 100), ForceMode.VelocityChange);

        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector3 position = transform.position;
        position.x = position.x + 0.1f * horizontal * Time.deltaTime * 250;
        transform.position = position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(isGroundedTrans.position, 0.1f);
    }
}

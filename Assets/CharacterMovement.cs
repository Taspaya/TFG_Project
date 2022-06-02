using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;

    [SerializeField]
    private Rigidbody myRb;

    [SerializeField]
    private ParticleSystem dsut;

    Quaternion m_Rotation = Quaternion.identity;
    public float turnSpeed = 20f;
    Vector3 m_Movement;
    Vector3 input;
    void FixedUpdate()
    {
        ManageInput();
        ManageAnimations();
    }


     
    void ManageInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); // Sets the bool to true if 'horizontal' input is approx 0
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards(myRb.transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        myRb.MovePosition(myRb.position + m_Movement * speed * Time.deltaTime);
        myRb.MoveRotation(m_Rotation);
    }

    void ManageAnimations()
    {
        GetComponent<Animator>().SetBool("Walking", input[0] != 0 || input[1] != 0);
    }


    public void SpawnDust()
    {
        dsut.Play();
    }
}

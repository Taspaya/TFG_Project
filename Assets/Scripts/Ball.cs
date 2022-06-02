using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Rigidbody myRb;

    Vector3 myDirection;

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        myRb.velocity = Vector3.forward * 10;
        myRb.MoveRotation(Quaternion.LookRotation(myDirection));
    } 
     



    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);        
    }
}
 
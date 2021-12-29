using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_footStep : MonoBehaviour
{
    [SerializeField]
    GameObject soundPrefab;

    [SerializeField]
    bool debug = false;
    private void OnTriggerEnter(Collider other)
    {
        if(debug) Debug.Log("PatPat");

        Instantiate(soundPrefab);
    
    }
}

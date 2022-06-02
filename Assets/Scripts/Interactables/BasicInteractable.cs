using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BasicInteractable : InteractableBase
{

    [SerializeField]
    UnityEvent onExit;

    [SerializeField]
    UnityEvent onClic;


    private void Update()
    {
        if (isInto && Input.GetMouseButtonDown(1))
        {
            onClic.Invoke();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
        isInto = false;
    }


}

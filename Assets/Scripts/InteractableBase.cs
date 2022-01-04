using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
 
    public bool compareTag = false;

    [SerializeField]
    string tagToCompare = "";

   [SerializeField]
   public UnityEvent customEvent;

    public virtual void ExecuteAction()
    {
        customEvent.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (compareTag)
        {
            if (other.tag == tagToCompare) ExecuteAction();
        }
        else ExecuteAction();
    }
}

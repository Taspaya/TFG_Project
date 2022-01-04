using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Pickup : InteractableBase
{
    // Start is called before the first frame update
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        Destroy(gameObject); 
    }
}

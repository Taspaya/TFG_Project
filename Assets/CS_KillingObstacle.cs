using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_KillingObstacle : InteractableBase
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject RespawnPoint;
    public override void ExecuteAction() {

        PlayerController.Instance.transform.position = RespawnPoint.transform.position;
    }

}

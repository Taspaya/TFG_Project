using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Pickup : InteractableBase
{
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        Instantiate(PlayerController.Instance.particles[1],transform).Play();
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Cs_Destructible : InteractableBase
{
    [SerializeField]
    int hits = 0;
    int hitsLeft;

    private void Start()
    {
        hitsLeft = hits;
    }

    public void Hit()
    {
        PlayerController.Instance.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        ExecuteAction();
    }
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        PlayerController.Instance.particles[0].Play();
        hitsLeft--;
        DestroyCheck();
    }

    private void DestroyCheck()
    {
        if (hitsLeft < 0) Destroy(gameObject);
    }
}

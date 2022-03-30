using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Cs_Destructible : InteractableBase
{
    [SerializeField]
    int hits = 0;
    int hitsLeft;

    [SerializeField]
    GameObject OnHitParticles;
    [SerializeField]
    GameObject OnDestroyParticles;
    [SerializeField]
    Transform particlePos;

    private void Start()
    {
        hitsLeft = hits;
    }

    public void Hit()
    {
       if (OnHitParticles) Instantiate(OnHitParticles, transform.position, Quaternion.identity, transform);

        PlayerController.Instance.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        ExecuteAction();
    }
    public override void ExecuteAction()
    {
        base.ExecuteAction();
        //PlayerController.Instance.particles[0].Play();
        hitsLeft--;
        DestroyCheck();
    }

    private void DestroyCheck()
    {
        if (hitsLeft < 0)
        {
            if (OnDestroyParticles) Instantiate(OnDestroyParticles, particlePos.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}

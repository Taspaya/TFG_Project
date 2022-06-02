using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public void AnimationEventAttack()
    {
        //Called in the end of the "Push" animation of the Player
        PlayerController.Instance.Attack();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInChildren<Animator>().SetTrigger("Attack");
            Base_Patrol me = GetComponentInParent<Base_Patrol>();
            me.DealDamage(other.transform.GetComponentInParent<Cs_CombatUnit>());
        }
    }
}

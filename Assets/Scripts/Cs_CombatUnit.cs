using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cs_CombatUnit : MonoBehaviour
{
    int maxLife;
    int currentLife;
    bool isStunned;

    int currentDamage;
    public abstract void Attack();
    public void RecieveDamage(int n) {
        currentLife -= n;
    }
    public void Heal(int n)
    {
        currentLife += n;
    }

    public void DealDamage(Cs_CombatUnit other)
    {
        other.RecieveDamage(currentDamage);
    }
}

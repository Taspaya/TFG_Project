using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cs_CombatUnit : MonoBehaviour
{
    public int maxLife { get; set; }
    public int currentLife { get; set; }
    public int strength { get; set; }
    public Vector3 direction { get; set; }
    public bool canMove { get; set; }


    [Header(" ======= COMBAT =========")]
    [System.NonSerialized]
    public bool isStunned;

    public int currentDamage = 1;

    private void Awake()
    {
        currentLife = maxLife;
    }

    public abstract void Attack();
    public void RecieveDamage(int n) {
        Debug.Log(gameObject.name + ": Ouch");
        currentLife -= n;
        DeathChecker();
    }

    public void Init_BasePatrol()
    {
        maxLife = 1;
        currentLife = 1;
        strength = 1;
        canMove = true;
    }
    void  DeathChecker()
    {
        if (currentLife <= 0) Destroy(gameObject);
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

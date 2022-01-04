using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cs_CombatUnit : MonoBehaviour
{
    [Header(" ======= COMBAT =========")]
    public int maxLife = 2;
    [System.NonSerialized]
    public int currentLife;
    [System.NonSerialized]
    public bool isStunned;

    public int currentDamage = 1;

    private void Start()
    {
        currentLife = maxLife;
    }
    public abstract void Attack();
    public void RecieveDamage(int n) {
        Debug.Log("I'm " + gameObject.name + "And I have: " + currentLife + " life");
        currentLife -= n;
        Debug.Log("I'm " + gameObject.name + "And i'm recieving: " + n + " Of damage. I have " + currentLife + " now");

        DeathChecker();
    }


    void  DeathChecker()
    {
        if (currentLife < 0) Destroy(gameObject);
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

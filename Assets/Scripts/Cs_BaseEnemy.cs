using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cs_BaseEnemy : Cs_CombatUnit
{

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxLife = 2;
        currentLife = maxLife;
    }

}

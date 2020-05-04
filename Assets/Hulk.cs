using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hulk : Enemy
{
    public int punchDamage = 2; // hulk have higher attack
    public bool isPunching;

    [SerializeField]
    int tempDamage;

    protected override void Start()
    {
        base.Start();
        tempDamage = damageToPlayer;
    }

    public override void Attack()
    {
        base.Attack();
        isPunching = true;
    }

    public void resetDamage()
    {
        damageToPlayer = tempDamage;
    }

    
    protected override void Update()
    {

        base.Update();

        if(isPunching)
        {
            damageToPlayer = punchDamage;
        }

    }

}

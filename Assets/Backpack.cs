using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : WeaponInventory
{
    
    protected override Weapon findWeapon()
    {
        int randomIndex = Random.Range(0, possibleWeapon.Length - 1);
        Weapon obj = possibleWeapon[randomIndex];
        return obj;
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    public Weapon[] possibleWeapon;
    public Weapon weapon;

    [SerializeField]
    GameObject hand;

    [SerializeField]
    protected Vector3 position;

    public bool hasPickedUP;


    protected virtual void Start()
    {
        findHand();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPickedUP)
        {
            weaponPickup();
        }

    }

    protected virtual void findHand()
    {
        hand = GameObject.FindGameObjectWithTag("Hand");
    }


    protected virtual void weaponPickup()
    {
        weapon = findWeapon();
        int itemNumber = weaponNumber();
        position = weapon.prefab.transform.position;

        // change position
        GameObject obj = Instantiate(weapon.prefab, transform.position, Quaternion.identity);
        obj.transform.parent = hand.transform;
        obj.transform.localPosition = position;
        obj.transform.localScale = hand.transform.localScale;

        // switch current weapon held
        switchWeapon(itemNumber, obj);

        // disable sprite
        GetComponent<SpriteRenderer>().enabled = false;

        // cannot pickup again
        hasPickedUP = true;
    }

    protected virtual Weapon findWeapon()
    {
        return possibleWeapon[0];
    }

    protected virtual int weaponNumber()
    {
        return weapon.itemNumber;
    }

    protected virtual void switchWeapon(int itemNumber, GameObject obj)
    {
        hand.GetComponent<WeaponSwitch>().selectLastWeapon();
        hand.GetComponent<WeaponSwitch>().weaponSelect();
        hand.GetComponent<Animator>().SetFloat("Weapon", itemNumber);
    }
}

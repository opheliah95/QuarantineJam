using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]

public class Weapon : ScriptableObject
{
    public string weaponName;
    public string spanishName;
    public Sprite handImage;
    public int itemNumber;
    public int ammoCount;
    public bool hasAmmoLimit;
    public GameObject prefab;

}

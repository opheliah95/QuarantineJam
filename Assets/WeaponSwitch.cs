
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;

    [SerializeField]
    int selectedWeapon = 0;
    public GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        weaponSelect();

    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        // go to next weapon
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }


        // go to previous weapon
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
            weaponSelect();
    }

    // function for select the weapon
    void weaponSelect()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.gameObject;
            }
                
            else
                weapon.gameObject.SetActive(false);

            i++;

        }
    }


   
}

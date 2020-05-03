using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [SerializeField]
    public Transform weaponHead;

    [SerializeField]
    protected GameObject shoots;

    public float coolOffTimer;

    [SerializeField]
    protected Controller controls;

    [SerializeField]
    protected float coolOff;

    private void Awake()
    {
        controls = new Controller();
        controls.Player.Shoot.performed += ctx => Shoot();
    }

    // Start is called before the first frame update
    void Start()
    {
        coolOff = coolOffTimer;
        weaponHead = gameObject.transform.GetChild(0);
    }



    void Shoot()
    {
        if (coolOff <= 0)
        {

            Instantiate(shoots, weaponHead.position, transform.rotation);
            coolOff = coolOffTimer;
        }
        else
        {
            coolOff -= Time.deltaTime;
        }
    }



    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

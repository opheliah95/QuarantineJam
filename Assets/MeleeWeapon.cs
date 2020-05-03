using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    GameObject player;

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Shoot()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        player.GetComponent<Animator>().SetTrigger("Stab");
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void disbleCollider()
    {
        GetComponent<CircleCollider2D>().enabled = false;
    }

    
}

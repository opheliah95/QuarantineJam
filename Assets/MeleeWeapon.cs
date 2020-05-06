using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    GameObject player;
    GameObject arm;
    public Transform attackPos;
    public int damage = 1;
    public float attackRange;
    public LayerMask whatIsEnemy;
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
        arm = GameObject.FindGameObjectWithTag("Hand");
    }

    void Shoot()
    {
        if (PlayerManager.isTalking)
            return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

        foreach(Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<Enemy>().takeDamage(damage);

        }

        arm.GetComponent<Animator>().SetTrigger("Stab");
        SoundManager.playSound("Knife");
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}

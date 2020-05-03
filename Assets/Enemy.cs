using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 2;
    public int damageToPlayer = 1;
    public float speed = 3;
    public float waitTime = 1f;
    public float playerRadius = 0.5f;
    public Transform[] moveSpots;

    [SerializeField]
    protected Transform player;
    [SerializeField]
    protected int randomSpot;
    [SerializeField]
    protected float minOffset = 0.2f;
    [SerializeField]
    protected float currentWaitTime;
    [SerializeField]
    bool collided = false;

    public bool facingRight;

    private void Start()
    {
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaitTime = waitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    private void Update()
    {
        if (!isInRange())
            Patrol();
        else
            Attack();

    }

    public bool isInRange()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        return (distance <= playerRadius);
    }

    void retreat()
    {
        flipEnemy(); // turns around
        float offset = facingRight ? Random.Range(10, 20) : Random.Range(-20, -10);
        Vector3 distanceOffset = new Vector3(offset, 0, 0);
        Debug.Log("will retreat...");
        transform.position = Vector2.MoveTowards(transform.position, transform.position + distanceOffset, speed * Time.deltaTime);
    }


    public void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        GetComponent<Animator>().SetTrigger("Attack");
    }

    public void Patrol()
    {
        Vector3 target = moveSpots[randomSpot].position;
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,target) <= minOffset)
        {
            if (currentWaitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                currentWaitTime = waitTime;
            }
               
            else
                currentWaitTime -= Time.deltaTime;

        }

        float difference = target.x - transform.position.x;
        if(difference > 0 && !facingRight || difference < 0 && facingRight)
        {
            flipEnemy();
        }
    }


    public void flipEnemy()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public void takeDamage(int damage)
    {

        health -= damage;
        
        if(health <= 0)
            Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.playSound("Hurt");
            HealthVisual.heartHealthSystem.Damage(damageToPlayer);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            collided = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collided = false;
        }
    }

   


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerRadius);
    }





}

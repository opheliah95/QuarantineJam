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
    public Transform range;
    public Transform[] moveSpots;

    [SerializeField]
    protected Transform player;
    [SerializeField]
    protected int randomSpot;
    [SerializeField]
    protected float minOffset = 0.2f;
    [SerializeField]
    protected float currentWaitTime;
   

    public bool facingRight;

    protected virtual void Start()
    {
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentWaitTime = waitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    protected virtual void Update()
    {
        if (PlayerManager.isTalking)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }

        basicBehaviours();
        
    }

    public virtual void basicBehaviours()
    {
        if (!isInRange())
            Patrol();
        else
            Attack();
    }

    
    public virtual bool isInRange()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        return (distance <= playerRadius);
    }

    public virtual void Attack()
    {
        facingPlayer();
        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        MoveTowardsPlayer();
        GetComponent<Animator>().SetTrigger("Attack");
    }

    protected virtual void MoveTowardsPlayer()
    {
        Vector3 targetPos = player.position;
        targetPos.y = transform.position.y;

        // Get a direction vector from us to the target
        Vector3 dir = targetPos - transform.position;

        // Normalize it so that it's a unit direction vector
        dir.Normalize();

        // Move ourselves in that direction
        transform.position += dir * speed * Time.deltaTime;
    }

    protected virtual void facingPlayer()
    {
        if (player.position.x > transform.position.x && !facingRight) //if the target is to the right of enemy and the enemy is not facing right
            flipEnemy();
        if (player.position.x < transform.position.x && facingRight)
            flipEnemy();
    }

    public virtual void Patrol()
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


    public virtual void flipEnemy()
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

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.playSound("Hurt");
            HealthVisual.heartHealthSystem.Damage(damageToPlayer);
        }

        // ignore collision with enemy
        if(collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

   

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(range.position, playerRadius);
    }


    private void OnDestroy()
    {
        if(GetComponent<ConditionTriggeredDialogue>()!=null)
        {
            List<Dialogue> dial = GetComponent<ConditionTriggeredDialogue>().dialogues;
            FindObjectOfType<DialogueManager>().startDialogue(dial);
           
        }
    }




}

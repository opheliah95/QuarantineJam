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

    private void Start()
    {
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


    public void Attack()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
    }


    public void takeDamage(int damage)
    {
        Debug.Log("Health is: " + health);

        health -= damage;
        
        if(health <= 0)
            Destroy(gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player damaged");
            HealthVisual.heartHealthSystem.Damage(damageToPlayer);
        }
    }
    

    


}

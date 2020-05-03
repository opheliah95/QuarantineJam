using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 3.0f;

    public float lifetime = 1.0f;

    public int damage = 1;

    public float distance;

    public LayerMask whatIsSoild;

    private void Start()
    {
        fly();
        Invoke("DestroyProjectile", lifetime);
    }

    void fly()
    {
        Transform head = GameObject.FindWithTag("Player").GetComponent<PlayerManager>().transform;
        float dir = head.localScale.x;
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed * dir;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, whatIsSoild);
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.forward* speed * Time.deltaTime);

        if(hit.collider != null)
        {
           
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
            }

            DestroyProjectile();

        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

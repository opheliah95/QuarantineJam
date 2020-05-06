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

    protected virtual void Start()
    {
        fly();
        Invoke("DestroyProjectile", lifetime);
    }

    protected virtual void fly()
    {
        Transform head = FindObjectOfType<PlayerManager>().transform;
        float dir = head.localScale.x;
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.right * speed * dir;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, whatIsSoild);

        if(hit.collider != null)
        {
           
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
            }

            DestroyProjectile();

        }
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : Bullet
{

    protected override void Start()
    {
        Invoke("DestroyProjectile", lifetime);
    }

    // Update is called once per frame
    protected override void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, distance, whatIsSoild);
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.forward* speed * Time.deltaTime);

        if (hit.collider != null)
        {

            if (hit.collider.CompareTag("Player"))
            {
                
                HealthVisual.heartHealthSystem.Damage(damage);
            }

            DestroyProjectile();

        }
    }

  
}

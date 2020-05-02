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
        Invoke("DestroyProjectile", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSoild);
        transform.Translate(transform.up * speed * Time.deltaTime);

        if(hit.collider != null)
        {
           
            if(hit.collider.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().takeDamage(damage);
                Debug.Log(hit.collider.gameObject.GetComponent<Enemy>().health);
            }

            DestroyProjectile();

        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

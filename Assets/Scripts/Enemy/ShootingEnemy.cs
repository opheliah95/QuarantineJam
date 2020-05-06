using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{
    public GameObject hand;
    public GameObject[] possibleWeapons;
    public GameObject bulletPrefab;
    public float coolOffPeriod = 0.3f;
    public float shootingDuration;

    protected override void Start()
    {
        base.Start();
        randomWeapon();
    }


    public override void Attack()
    {
        // facing player
        base.facingPlayer();

        // rotate arm at player
        rotateArmAtPlayer();

        // shoot bullets at player
        if(shootingDuration >= coolOffPeriod)
        {
            shootBullets();
            shootingDuration = 0;
        }
        else
        {
            shootingDuration += Time.deltaTime;
        }
           

    }


    void rotateArmAtPlayer()
    {
        Vector3 difference = player.position - transform.position;
        difference.Normalize(); // normaalize value

        // get the angle of rotation
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // rotate when on tricky position
        if (rotationAngle <= 90 && rotationAngle >= -90)
            hand.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        else if ((rotationAngle <= -90 || rotationAngle >= 90))
            hand.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle + 180);
    }

    void shootBullets()
    {
        Transform gunPoint = hand.transform.GetChild(0).GetChild(0); // get the shooting point
        GameObject obj = Instantiate(bulletPrefab, gunPoint.transform.position, hand.transform.rotation);
        float xDir = transform.localScale.x;
        obj.GetComponent<Rigidbody2D>().velocity = gunPoint.transform.right * speed * xDir;
        SoundManager.playSound("Shoot");
    }

    void randomWeapon()
    {
        int randomWeaponIndex = Random.Range(0, possibleWeapons.Length - 1);
        GameObject weapon = possibleWeapons[randomWeaponIndex];
        // change position
        GameObject obj = Instantiate(weapon);
        obj.transform.SetParent(hand.transform);
        obj.transform.localPosition = weapon.transform.position;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = hand.transform.localScale;

    }
}

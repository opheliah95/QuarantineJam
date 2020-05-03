﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float moveSpeed = 5f;

    public int backPackState = 0;

    public int damage = 1;
    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Controller controls;

    [SerializeField]
    Vector2 movementInput;

    [SerializeField]
    Animator animController;

    public bool facingRight;

    private void Awake()
    {
        facingRight = true;
        controls = new Controller();
        animController = GetComponent<Animator>();
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Player.Jump.performed += ctx => Jump();
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        animController.SetFloat("BackPack", backPackState);
        Move();
    }


    public void Move()
    {
        float x = movementInput.x;
        rb2d.transform.position += new Vector3(x, 0, 0) * Time.deltaTime * moveSpeed;
        bool moved = (x == 0) ? false : true;
        flipRight(x);
        animController.SetBool("isWalking", moved);

    }


    public void Jump()
    {
        rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        animController.SetTrigger("isJumping");
    }

    void flipRight(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            
            flipCharacterAndWeapon();
           
        }
    }

    public void flipCharacterAndWeapon()
    {

        flipCharacterOnly();

        // hand script
        GameObject hand = GameObject.FindGameObjectWithTag("Hand");
        GameObject weaponHead = hand.GetComponent<WeaponSwitch>().currentWeapon.transform.GetChild(0).gameObject;
        Debug.Log(weaponHead);
        Vector3 theScale = weaponHead.transform.localScale;
        theScale.x *= -1;
        weaponHead.transform.localScale = theScale;
    }

    public void flipCharacterOnly()
    {
        // flip character
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

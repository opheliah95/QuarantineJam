using System.Collections;
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

    [SerializeField]
    Sprite backPackIdle;

    public bool isGrounded;

    private void Awake()
    {
        isGrounded = true;
        facingRight = true;
        controls = new Controller();
        animController = GetComponent<Animator>();
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        //controls.Player.Jump.performed += ctx => Jump();
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        animController.SetFloat("BackPack", backPackState);
        Move();

        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            Jump();
        }
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
        isGrounded = false;
    }

    void flipRight(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            
            flipCharacterOnly();
           
        }
    }

    public void flipCharacterAndWeapon()
    {

        flipCharacterOnly();

        // hand script
        GameObject hand = GameObject.FindGameObjectWithTag("Hand");
        /*
        GameObject weaponHead = hand.GetComponent<WeaponSwitch>().currentWeapon.transform.GetChild(0).gameObject;
        Debug.Log(hand.GetComponent<WeaponSwitch>().currentWeapon);
        */

        Vector3 theScale = hand.transform.localScale;
        theScale.x *= -1;
        hand.transform.localScale = theScale;
    }

    public void flipCharacterOnly()
    {
        // flip character
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void AddBackPack()
    {
        if (backPackState != 0)
        {
            if(animController.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
                GetComponent<SpriteRenderer>().sprite = backPackIdle;
        }
            
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
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

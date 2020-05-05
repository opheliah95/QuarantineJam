using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    LayerMask groundLayerMask;

    public float jumpHeight = 2f;
    public float moveSpeed = 5f;
    public float groundDistance = 0.1f;
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

    private BoxCollider2D boxCollider;

    public static bool isTalking;

    private void Awake()
    {
        
        controls = new Controller();
        animController = GetComponent<Animator>();
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        //controls.Player.Jump.performed += ctx => Jump();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        facingRight = true;
    }

    private void Update()
    {
        animController.SetFloat("BackPack", backPackState);

        if(isTalking)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;// you cannot move
            return;
        }

        Move();

        if(Input.GetKeyDown(KeyCode.W) && isGrounded())
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
        rb2d.velocity = Vector2.up * jumpHeight;
        animController.SetTrigger("isJumping");
    }


    private bool isGrounded()
    {
        // look down for ground
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, groundDistance, groundLayerMask);
        Debug.Log(hit.collider);
        return (hit.collider != null); // check if you hit something
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

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float moveSpeed = 5f;

    [SerializeField]
    Rigidbody2D rb2d;

    [SerializeField]
    Controller controls;

    [SerializeField]
    Vector2 movementInput;

    [SerializeField]
    Animator animController;


    [SerializeField]
    bool facingRight;

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
        Move();
    }
    
    public void Move()
    {
        float x = movementInput.x;
        rb2d.transform.position += new Vector3(x, 0, 0) * Time.deltaTime * moveSpeed;
        bool moved = (x == 0) ? false : true;
        flipRight(x);
        //animController.SetBool("isRunning", moved);

    }

    public void Jump()
    {
        rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    void flipRight(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
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

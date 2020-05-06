using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    Controller playerController;

    [SerializeField]
    Vector2 mouseMovement;

    public bool rotationDirRight = true;
    public bool justFlipped = false;

    public float countDownBeforeFlip;

    private void Awake()
    {
        playerController = new Controller();
        playerController.Player.Rotate.performed += ctx => mouseMovement = ctx.ReadValue<Vector2>();
    }


    private void Update()
    {
        // if the mouse does not move then do nothing
        if (((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0)) && !PlayerManager.isTalking)
        {
            Vector3 difference = offsetPosBetweenMouseAndCamera();
            flipCharacter(difference);
            rotateWeapon(difference);


        }

    }

    void countdownBeforeFlippingArm()
    {
        countDownBeforeFlip += Time.deltaTime;

        if(countDownBeforeFlip >= 0.1)
        {

            countDownBeforeFlip = 0;
            justFlipped = false;
        }
    }

    Vector3 offsetPosBetweenMouseAndCamera()
    {
        //distance between mouse and weapon pivot
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // normaalize value
        return difference;
    }


    bool rotateWeapon(Vector3 difference)
    {

        // get the angle of rotation
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // rotate item to mouse pointer
        
        if (rotationAngle <= 90 && rotationAngle >= -90)
            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

        else if ((rotationAngle < -90 || rotationAngle > 90))
            transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle + 180);



        return true;
        
       

    }
    void flipCharacter(Vector2 difference)
    {
        // flip character 
        rotationDirRight = player.GetComponent<PlayerManager>().facingRight;

        if (difference.x >= 0 && !rotationDirRight || (difference.x < 0 && rotationDirRight))
        { // mouse is on right side of player
            player.GetComponent<PlayerManager>().flipCharacter();
            justFlipped = true;
            return;
        }

        justFlipped = false;
       
    }

    void aramRotation()
    {
        Vector3 localScale = transform.localScale;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


   
}

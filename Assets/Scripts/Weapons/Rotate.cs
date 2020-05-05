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

    private void Awake()
    {
        playerController = new Controller();
        playerController.Player.Rotate.performed += ctx => mouseMovement = ctx.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {
        // if the mouse does not move then do nothing
        if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
        {
            Vector3 difference = offsetPosBetweenMouseAndCamera();

            if (flipCharacter(difference))
                return;

            rotateWeapon(difference);

        }
            

       
        
        
       

    }

    Vector3 offsetPosBetweenMouseAndCamera()
    {
        //distance between mouse and weapon pivot
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize(); // normaalize value
        return difference;
    }


    void rotateWeapon(Vector3 difference)
    {
       

        // get the angle of rotation
        float rotationAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        // rotate item to mouse pointer
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);


        // fix when mouse is on the other side of the body
        if ((rotationAngle < -90 || rotationAngle > 90))
        {
            

            if (player.transform.eulerAngles.y == 0)
            {
                transform.localRotation = Quaternion.Euler(180, 0, -rotationAngle);
            }
            else if (player.transform.eulerAngles.y == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationAngle);
            }

        }

        
       

    }
    bool flipCharacter(Vector2 difference)
    {
        // flip character 
        rotationDirRight = player.GetComponent<PlayerManager>().facingRight;
        if (difference.x >= 0 && !rotationDirRight || (difference.x < 0 && rotationDirRight))
        { // mouse is on right side of player
            player.GetComponent<PlayerManager>().flipCharacter();

            Vector3 localScale = transform.localScale;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;

            return true;
        }

        return false;
       
    }


   
}

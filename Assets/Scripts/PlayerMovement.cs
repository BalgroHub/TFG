using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public float playerSpeed;
    public float maxSpeed;
    public PlayerInput playerInput;

    //public float playerSpeedAceleration;

    /*
    private float acelerationX;
    private float acelerationY;

    private float decelerationX;
    private float decelerationY;
    */



    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        /*
        acelerationX = 0f;
        acelerationY = 0f;

        decelerationX = 0f;
        decelerationY = 0f;
        */
    }

    /*
    void FixedUpdate()
    {

        //Horizontal with aceleration
        float horizontalForce = Input.GetAxis("Horizontal");
        if (horizontalForce > 0)
        {
            acelerationX = .2f * horizontalForce;
        }

        if (horizontalForce < 0)
        {
            acelerationX = .2f * horizontalForce;
        }

        if (horizontalForce == 0)
        {
            acelerationX = 0f;
        }


        //Vertical with aceleration
        float verticalForce = Input.GetAxis("Vertical");
        if (verticalForce > 0)
        {
            acelerationY = .2f * verticalForce;
        }

        if (verticalForce < 0)
        {
            acelerationY = .2f * verticalForce;
        }

        if (verticalForce == 0)
        {
            acelerationY = 0f;
        }

        Vector2 newVelocity2 = playerRb.velocity;

        //In the non touched axis, we create an artificial deceleration
        if (acelerationX == 0f)
        {
            //Varying if the velocity is positive or negative
            if (newVelocity2.x > 0f)
            {
                if (newVelocity2.x - 1.5f <= 0f)
                {
                    newVelocity2.x = 0f;
                    decelerationX = -0f;
                }
                else decelerationX = -.2f;
            }
            if (newVelocity2.x < 0f)
            {
                if (newVelocity2.x + 1.5f >= 0f)
                {
                    newVelocity2.x = 0f;
                    decelerationX = 0f;
                }
                else decelerationX = .2f;
            }
        }
        else
        {
            decelerationX = 0f;
        }

        if (acelerationY == 0f)
        {
            //Varying if the velocity is positive or negative
            if (newVelocity2.y > 0f)
            {
                
                if (newVelocity2.y - 1.5f <= 0f)
                {
                    newVelocity2.y = 0f;
                    decelerationY = 0f;
                }
                else decelerationY = -.2f;
            }
            if (newVelocity2.y < 0f)
            {
                
                if (newVelocity2.y + 1.5f >= 0f)
                {
                    newVelocity2.y = 0f;
                    decelerationY = 0f;
                }
                else decelerationY = .2f;
            }
        }
        else
        {
            decelerationY = 0f;
        }

        //We apply the aceletarion and deceleration
        newVelocity2.x += playerSpeed * acelerationX;
        newVelocity2.y += playerSpeed * acelerationY;

        newVelocity2.x += playerSpeed * decelerationX;
        newVelocity2.y += playerSpeed * decelerationY;

        //And limit the speed in both axis
        if (newVelocity2.x > maxSpeed) newVelocity2.x = maxSpeed;
        if (newVelocity2.x < -maxSpeed) newVelocity2.x = -maxSpeed;

        if (newVelocity2.y > maxSpeed) newVelocity2.y = maxSpeed;
        if (newVelocity2.y < -maxSpeed) newVelocity2.y = -maxSpeed;

        playerRb.velocity = newVelocity2;

    }
    */

    private void FixedUpdate()
    {
        //Calculate the velocity of the player
        Vector2 horizontalValue = playerInput.actions["move"].ReadValue<Vector2>();
        //float horizontalMovement = Input.GetAxis("Horizontal");


        Vector2 newVelocity = new Vector2(horizontalValue.x * playerSpeed, 0);

        //Cap it with the max velocity
        newVelocity = new Vector2( Mathf.Clamp(newVelocity.x, -maxSpeed, maxSpeed),   newVelocity.y  );

        //Apply it
        playerRb.velocity = newVelocity;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private SpriteRenderer playerSP;
    public List<Sprite> mySprites;
    private PlayerMovement playerMSC;

    void Start()
    {
        playerRB  = GetComponent<Rigidbody2D>();
        playerSP  = GetComponent<SpriteRenderer>();
        playerMSC = GetComponent<PlayerMovement>();


    }

    void Update()
    {
        //Calculate the middle frame(ej: 29/2 = 14.5 = 14 , knowing that the list starts with 0)
        int middleFrame = mySprites.Count / 2;

        //We changue the sprite depending of the velocity in x
        if (playerRB.velocity.x < 0f)
        {
            //Debug.Log((int)playerRb.velocity.x * 14 / 10);
            playerSP.sprite = mySprites[ (int)playerRB.velocity.x * middleFrame / (int)playerMSC.maxSpeed + middleFrame ];
        }
        if (playerRB.velocity.x > 0f)
        {
            //Debug.Log((int)playerRb.velocity.x * 14 / 10);
            playerSP.sprite = mySprites[ (int)playerRB.velocity.x * middleFrame / (int)playerMSC.maxSpeed + middleFrame ];
        }
        if(playerRB.velocity.x == 0f)
        {
            //With 0 velocity, we put the middle frame
            playerSP.sprite = mySprites[ middleFrame ];
        }
    }
}

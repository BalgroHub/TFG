using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float life;
    public GameObject floatingTextPrefab;
    public float contactDamage;

    //Bounce
    public Vector2 startingDirection;

    protected virtual void Awake()
    {
        life = 10000f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If its a Playerbullet
        if (collision.gameObject.layer == 9)
        {
            //Print out in a floating text the damage dealt
            float Bulletdamage = collision.gameObject.GetComponent<Bullet>().damage;

            //Instanciate a FloatingText prefab in an upper position of the player(FloatingTextTransform)
            Transform FloatingTextTransform = transform.Find("FloatingTextTransform");

            if(floatingTextPrefab)
            {
                GameObject newFloatingText = Instantiate(floatingTextPrefab, FloatingTextTransform);

                //We round the damage number to the integer
                newFloatingText.GetComponent<FloatingText>().setText("" + Mathf.Round(Bulletdamage));
            }

            //Deal the damage
            takeDamage(Bulletdamage);

            //Destroy the bullet
            Destroy(collision.gameObject);
        }

        //If its a wall (layer 13)
        if (collision.gameObject.layer == 13)
        {
            //We invert the x velocity, making it like a bounce off the wall
            Vector2 actualVelocity = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = actualVelocity * new Vector2(-1f, 1f);
        }

        //If its the lifezone (layer 14)
        //We take contact damage of the enemy and then the enemy is destroyed
        if(collision.gameObject.layer == 14)
        {
            PlayerStats.Instance.takeDamage(contactDamage);
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// The enemy takes damage and if the life reaches 0, dies
    /// </summary>
    /// <param name="damage"></param>
    public void takeDamage(float damage)
    {
        life -= damage;
        if(life <= 0)
        {
            //Death animation gif
            Destroy(this.gameObject);
        }
    }

    
    

    protected virtual void Update()
    {
    }
}

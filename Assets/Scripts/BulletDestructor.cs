using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestructor : MonoBehaviour
{

    /// <summary>
    /// If a gameobject with the layer Player bullet or Enemy Bullet hit this colision, the bullet its destroyed
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {

            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBig : Enemy
{

    public bool isShooting;
    public bool canShoot;
    public float shootInterval;
    public float bulletDamage;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public Transform bulletsParent;

    
    protected override void Awake()
    {
        life = 12;
        isShooting = false;
        contactDamage = 40;

        bulletsParent = transform.parent.parent.Find("Bullets").Find("EnemyBullets");

        StartCoroutine(StartShooting());
    }


    protected override void Update()
    {
        base.Update();
        if (!isShooting)
        {
            //StartCoroutine(Shooting());
            //StartCoroutine(StartShooting());
        }
    }

    IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(1.35f);
        StartCoroutine(Shooting());
    }


    /// <summary>
    /// Shoots if able
    /// </summary>
    /// <returns></returns>
    IEnumerator Shooting()
    {
        while (canShoot == true)
        {
            isShooting = true;
            Shoot();
            yield return new WaitForSecondsRealtime(shootInterval);
        }

        isShooting = false;
    }

    /// <summary>
    /// Generates a bullet with everything
    /// </summary>
    void Shoot()
    {

        GameObject thisBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletsParent);
        thisBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(thisBullet.GetComponent<Rigidbody2D>().velocity.x, -bulletSpeed);
        thisBullet.GetComponent<Bullet>().damage = bulletDamage;
    }


}

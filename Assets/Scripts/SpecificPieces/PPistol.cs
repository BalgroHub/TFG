using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPistol : Piece
{
    public float damageBase;
    public float damageModified;

    public float shootIntervalBase;
    public float shootIntervalModified;

    public float bulletSpeed;
    public GameObject bullet;

    public Transform playerPos;

    public bool canShoot;
    private bool isShooting = false;

    public Transform bulletsParent;


    private void Start()
    {
        //We assing each modified value
        shootIntervalModified = shootIntervalBase;
        damageModified = damageBase;

        playerPos = PlayerStats.Instance.gameObject.transform;
        bulletsParent = GameObject.Find("PlayerBullets").transform;
    }

    void Update()
    {
        //Creamos una subrutina, que cada shootInterval en segundos, hace la funcion Shoot
        if(!isShooting)
        {
            StartCoroutine(Shooting());
        }

    }

    public override void TypeActions()
    {
        //First we reset every stat
        damageModified = damageBase;
        shootIntervalModified = shootIntervalBase;

        StartCoroutine(Shooting());
    }

    /// <summary>
    /// Shoots if able
    /// </summary>
    /// <returns></returns>
    IEnumerator Shooting()
    {
        while(canShoot == true && active)
        {
            isShooting = true;
            Shoot();
            yield return new WaitForSeconds(shootIntervalModified);
        }

        isShooting = false;
    }

    /// <summary>
    /// Generates a bullet and everything
    /// </summary>
    void Shoot()
    {
        //Instanciate a bullet, with the damage and velocity indicated on this weapon
        GameObject thisBullet = Instantiate( bullet, playerPos.position, Quaternion.identity, bulletsParent);
        thisBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(thisBullet.GetComponent<Rigidbody2D>().velocity.x, bulletSpeed);
        thisBullet.GetComponent<Bullet>().damage = damageModified;
    }

    public override void ModifyAttSpe(float multiplier)
    {
        shootIntervalModified = shootIntervalBase / ((float)multiplier + 1);

        //Debug.Log("" + shootIntervalBase + " / " + multiplier + " = " + calculation);
    }

    public override void ModifyDamage(float multiplier)
    {
        damageModified = damageBase * (multiplier+1);
    }
}

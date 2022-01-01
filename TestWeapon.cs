using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    public float fireRate;
    private float lastShot;

    public Bullet bullet;
    public float bulletVelocity;

    public Transform firePoint;

    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if (Time.time > lastShot)
            {
                Bullet _bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

                Rigidbody2D rb = _bullet.GetComponent<Rigidbody2D>();

                rb.AddForce(firePoint.right * bulletVelocity, ForceMode2D.Impulse);

                lastShot = Time.time + 1 / fireRate;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform playerTransform;
    public float bulletSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        PlayerSound.GunSound();
        // Click to pos
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // cal direction
        Vector2 direction = (mousePosition - (Vector2)playerTransform.position).normalized;

        FireBullet(direction);
    }

    void FireBullet(Vector2 direction)
    {
        // make bullet
        GameObject bullet = Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * bulletSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXBoss : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 20f;
    public float fireInterval = 2f;
    public Slider BossHpBar;

    private float BossHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootRandomBullet", 0f, fireInterval);
    }
    void ShootRandomBullet()
    {
        //random
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // to vec2
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // fire bullet
        FireBullet(randomDirection);
    }
    void FireBullet(Vector2 direction)
    {
        // create bullet
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * bulletSpeed;
        }
    }
    public void getDamage(int dmg) // get Damage to Boss
    {
        BossHP -= dmg;
        BossHpBar.value = BossHP / 100;
    }
}

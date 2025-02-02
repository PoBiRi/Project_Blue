using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMan_BOMB : EnemyBullet
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public override void OutBullet()
    {
        bool flag = false;
        Destroy(gameObject);
        float bulletSpeed = 5f;
        int bulletCount = 8;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            // create bullet
            GameObject bullet = Instantiate(flag ? bulletPrefab : bulletPrefab2, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * bulletSpeed;
            }
            flag = !flag;
        }
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            OutBullet();
        }

        if (other.CompareTag("Player")) // meet player
        {
            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(10);
        }
    }
}

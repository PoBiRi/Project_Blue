using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_UnDestroy : EnemyBullet
{
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

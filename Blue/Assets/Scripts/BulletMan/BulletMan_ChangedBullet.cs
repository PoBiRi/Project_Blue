using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMan_ChangedBullet : EnemyBullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
    }
}

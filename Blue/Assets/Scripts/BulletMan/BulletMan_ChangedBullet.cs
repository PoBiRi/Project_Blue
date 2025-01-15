using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMan_ChangedBullet : EnemyBullet
{
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(getSpeed());
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            OutBullet();
        }

        if (other.CompareTag("Player")) // meet player
        {
            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(15);
        }
    }

    IEnumerator getSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            rb.velocity *= 1.1f;
        }
    }

}

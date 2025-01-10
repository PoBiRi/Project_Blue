using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMan_ChangedBullet : EnemyBullet
{
    private float pushForce = 40f;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // ĳ���͸� �Ѿ˿��� �־����� �������� �о
                Vector2 pushDirection = (Vector2)playerRb.position - (Vector2)transform.position;
                playerRb.velocity = Vector2.zero;  // ���� �ӵ� �ʱ�ȭ
                playerRb.AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
            }

            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
    }
}

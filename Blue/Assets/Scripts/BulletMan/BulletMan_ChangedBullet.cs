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
                // 캐릭터를 총알에서 멀어지는 방향으로 밀어냄
                Vector2 pushDirection = (Vector2)playerRb.position - (Vector2)transform.position;
                playerRb.velocity = Vector2.zero;  // 기존 속도 초기화
                playerRb.AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
            }

            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
    }
}

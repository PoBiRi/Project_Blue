using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastTamer_Cloud : EnemyBullet
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 45);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            //Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
        if (other.CompareTag("Wall"))
        {
            // 충돌한 지점의 법선 벡터 가져오기
            Vector2 normal = GetCollisionNormal(other);

            // 입사 벡터 (현재 속도)
            Vector2 incomingVelocity = rb.velocity;

            // 반사 벡터 계산
            Vector2 reflectVelocity = Vector2.Reflect(incomingVelocity, normal);

            // 새 속도 적용
            rb.velocity = reflectVelocity.normalized * 3f;
            Vector2 direction = rb.velocity;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 45);
        }
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
    }
    private Vector2 GetCollisionNormal(Collider2D other)
    {
        // 물체의 위치와 충돌 객체(벽)의 가장 가까운 점 간의 방향 계산
        Vector2 collisionPoint = other.ClosestPoint(transform.position);
        Vector2 normal = (transform.position - (Vector3)collisionPoint).normalized;

        return normal;
    }
}

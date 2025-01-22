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
            // �浹�� ������ ���� ���� ��������
            Vector2 normal = GetCollisionNormal(other);

            // �Ի� ���� (���� �ӵ�)
            Vector2 incomingVelocity = rb.velocity;

            // �ݻ� ���� ���
            Vector2 reflectVelocity = Vector2.Reflect(incomingVelocity, normal);

            // �� �ӵ� ����
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
        // ��ü�� ��ġ�� �浹 ��ü(��)�� ���� ����� �� ���� ���� ���
        Vector2 collisionPoint = other.ClosestPoint(transform.position);
        Vector2 normal = (transform.position - (Vector3)collisionPoint).normalized;

        return normal;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    protected virtual float radius { get; set; } = 10f;

    void Update()
    {
        if (IsOutOfCircle())
        {
            OutBullet();
        }
    }
    bool IsOutOfCamera() // if out of camera bullet collapse
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            return true;
        }

        return false;
    }
    bool IsOutOfCircle()
    {
        // (0, 0)�� ������Ʈ�� ��ġ ������ �Ÿ� ���
        float distanceFromCenter = Vector2.Distance(transform.position, Vector2.zero);

        // �Ÿ� ���� �������� �ʰ��ϸ� �� ������ ���� ������ �Ǵ�
        if (distanceFromCenter > radius)
        {
            return true;
        }

        // �� �ȿ� ���� ���
        return false;
    }

    public virtual void OutBullet()
    {
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // meet bullet collapse
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player")) // meet player
        {            
            Destroy(gameObject);
            
            GameObject.Find("Player").GetComponent<Player>().getDamage(3);
        }
    }
}

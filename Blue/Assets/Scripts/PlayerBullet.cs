using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    void Update()
    {
        if (IsOutOfCircle())
        {
            Destroy(gameObject);
        }
    }
    bool IsOutOfCamera()
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
        if (distanceFromCenter > 10f)
        {
            return true;
        }

        // �� �ȿ� ���� ���
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet")) // meet bullet collapse
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy")) // meet enemy
        {
            Destroy(gameObject);
            GameObject.FindWithTag("Enemy").GetComponent<Boss>().getDamage(10);
        }
    }
}

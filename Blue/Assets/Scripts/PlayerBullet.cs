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
        // (0, 0)과 오브젝트의 위치 사이의 거리 계산
        float distanceFromCenter = Vector2.Distance(transform.position, Vector2.zero);

        // 거리 값이 반지름을 초과하면 원 밖으로 나간 것으로 판단
        if (distanceFromCenter > 10f)
        {
            return true;
        }

        // 원 안에 있을 경우
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

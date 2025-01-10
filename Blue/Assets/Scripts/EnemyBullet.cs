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
        // (0, 0)과 오브젝트의 위치 사이의 거리 계산
        float distanceFromCenter = Vector2.Distance(transform.position, Vector2.zero);

        // 거리 값이 반지름을 초과하면 원 밖으로 나간 것으로 판단
        if (distanceFromCenter > radius)
        {
            return true;
        }

        // 원 안에 있을 경우
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

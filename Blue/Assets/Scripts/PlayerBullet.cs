using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    void Update()
    {
        if (IsOutOfCamera())
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // meet bullet collapse
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy")) // meet enemy
        {
            Destroy(gameObject);
            GameObject.Find("EXBoss").GetComponent<EXBoss>().getDamage(10);
        }
    }
}

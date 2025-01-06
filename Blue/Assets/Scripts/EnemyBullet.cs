using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        if (IsOutOfCamera())
        {
            Destroy(gameObject);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // meet bullet collapse
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player")) // meet player
        {
            Destroy(gameObject);
            
            GameObject.Find("Player").GetComponent<Player>().getDamage(10);
        }
    }
}

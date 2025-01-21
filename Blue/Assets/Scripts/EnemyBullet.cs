using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Rigidbody2D drb;

    private void Start()
    {
        drb = GetComponent<Rigidbody2D>();
        Vector2 direction = drb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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

        if (other.CompareTag("Wall"))
        {
            OutBullet();
        }

        if (other.CompareTag("Player")) // meet player
        {            
            Destroy(gameObject);
            
            GameObject.Find("Player").GetComponent<Player>().getDamage(5);
        }
    }
}

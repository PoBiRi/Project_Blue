using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastTamer_ZigZag : EnemyBullet
{
    private float speed = 5f;           // 총알 이동 속도
    private float delay = 0.5f;
    private bool flag = false;

    void Start()
    {
        StartCoroutine(MovingAfterDelay());
        Destroy(gameObject, 15f);
    }

    private IEnumerator MovingAfterDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);

            float randomX = Random.Range(-1f, 1f);
            float randomY = Random.Range(-1f, 1f);
            Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

            // create bullet
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                if (flag)
                {
                    rb.velocity = randomDirection * speed;
                    flag = false;
                }
                else
                {
                    rb.velocity = randomDirection * 0;
                    flag = true;
                }
            }
        }
    }
}

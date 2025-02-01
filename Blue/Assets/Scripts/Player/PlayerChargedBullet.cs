using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChargedBullet : PlayerBullet
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy")) // meet enemy
        {
            Destroy(gameObject);
            GameObject.FindWithTag("Enemy").GetComponent<Boss>().getDamage(2.5f);
        }
    }
}

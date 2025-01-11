using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BeastTamer_Wing : EnemyBullet
{
    public GameObject Bullet;

    private GameObject[] spawnedBullets;
    private int currentIndex = 0;
    private int maxBullets = 50;

    private void Start()
    {
        spawnedBullets = new GameObject[maxBullets];
        StartCoroutine(SpawnBullet());
    }

    protected override void OnTriggerEnter2D(Collider2D other)
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
            GameObject.Find("Player").GetComponent<Player>().getDamage(3);
        }
    }

    IEnumerator SpawnBullet()
    {
        while(true)
        {
            // ���ο� ������Ʈ ����
            GameObject newObject = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
            rb.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            // �迭�� ����
            spawnedBullets[currentIndex] = newObject;

            // �迭 �ε��� ��ȯ
            currentIndex = (currentIndex + 1) % maxBullets;

            // 0.1�� ���
            yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OutBullet()
    {
        Vector2 wingDirection = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        Vector2 direction = new Vector2(-wingDirection.y, wingDirection.x);
        foreach(GameObject obj in spawnedBullets) 
        {
            if(obj != null)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // apply speed and direction
                    rb.velocity = direction.normalized * 3f;
                }
            }
        }
        Destroy(gameObject);
    }
}
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
    public bool isRage;
    private Rigidbody2D rb;

    private void Start()
    {
        spawnedBullets = new GameObject[maxBullets];
        StartCoroutine(SpawnBullet());
        isRage = GameObject.Find("BeastTamer(Clone)").GetComponent<BeastTamer>().rageFlag;
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }


    protected override void OnTriggerEnter2D(Collider2D other)
    {
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
        Debug.Log(isRage);
        while (true)
        {
            // 새로운 오브젝트 생성
            GameObject newObject = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
            rb.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            // 배열 인덱스 순환
            currentIndex = (currentIndex + 1) % maxBullets;

            // 배열에 저장
            spawnedBullets[currentIndex] = newObject;

            if(isRage)
            {
                newObject = Instantiate(Bullet, transform.position, Quaternion.identity);
                rb = newObject.GetComponent<Rigidbody2D>();
                rb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                // 배열 인덱스 순환
                currentIndex = (currentIndex + 1) % maxBullets;

                // 배열에 저장
                spawnedBullets[currentIndex] = newObject;
            }


            // 0.1초 대기
            yield return new WaitForSeconds(0.4f);
        }
    }

    public override void OutBullet()
    {
        Vector2 wingDirection = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
        Vector2 direction = new Vector2(-wingDirection.y, wingDirection.x);
        int count = 0;
        foreach(GameObject obj in spawnedBullets) 
        {
            if(obj != null)
            {
                Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    if (isRage && count % 2 > 0)
                    {
                        rb.velocity = new Vector2(-direction.x, -direction.y).normalized * 3f;
                    }
                    else rb.velocity = direction.normalized * 3f;
                }
            }
            count++;
        }
        Destroy(gameObject);
    }
}
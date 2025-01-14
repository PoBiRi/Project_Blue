using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_Ring : MonoBehaviour
{
    private bool destroyFlag = false;
    public GameObject bulletPrefab;
    private float attractionForce; // 끌려가는 속도
    private float angle;
    private float distance;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 attractDirection;
    private float speed = 3f;
    private Vector2 spot; // 중심 지점
    private bool flag = false;

    void Start()
    {
        while(true){
            angle = Random.Range(0, 360) * Mathf.Deg2Rad; // 랜덤 각도
            spot = new Vector2(Mathf.Cos(angle) * 10f, Mathf.Sin(angle) * 10f);
            distance = Vector2.Distance((Vector2)transform.position, spot);
            if(distance < 15 && distance > 5)
            {
                break;
            }
        }
        float crossProduct = ((Vector2)transform.position).x * spot.y - ((Vector2)transform.position).y * spot.x;
        if (crossProduct > 0) flag = true;
        rb = GetComponent<Rigidbody2D>();
        attractionForce = rb.mass * Mathf.Pow(speed, 2) / distance;

        int bulletCount = 10;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * 0.5f, Mathf.Sin(Mathf.Deg2Rad * angle) * 0.5f);

            // create bullet
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Destroy(bullet.GetComponent<EnemyBullet>());
            bullet.AddComponent<BulletFromWall>();
            bullet.transform.SetParent(transform);
            bullet.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        attractDirection = (spot - (Vector2)transform.position).normalized;
        if (flag) direction = new Vector2(-attractDirection.y, attractDirection.x);
        else direction = new Vector2(attractDirection.y, -attractDirection.x);

        rb.velocity = direction * (speed) + (attractDirection * attractionForce * Time.deltaTime);

        /*rb.velocity = direction * speed;
        rb.AddForce(attractDirection * attractionForce);*/
        // 물체를 목표 위치로 이동
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if(destroyFlag) Destroy(gameObject);
            else destroyFlag = true;
        }
    }
}

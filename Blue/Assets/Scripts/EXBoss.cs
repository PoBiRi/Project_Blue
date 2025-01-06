using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXBoss : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 20f;
    public float fireInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootRandomBullet", 0f, fireInterval);
    }
    void ShootRandomBullet()
    {
        // 랜덤한 방향을 생성 (-1에서 1 사이)
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // 생성된 랜덤 방향 벡터
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // 랜덤 방향으로 총알 발사
        FireBullet(randomDirection);
    }
    void FireBullet(Vector2 direction)
    {
        // 총알을 발사 지점에서 생성
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 총알에 방향과 속도를 적용
            rb.velocity = direction * bulletSpeed;
        }
    }
}

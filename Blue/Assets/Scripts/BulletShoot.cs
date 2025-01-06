using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform playerTransform;
    public float bulletSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        // 마우스 클릭 위치를 월드 좌표로 변환
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 플레이어와 마우스 클릭 위치 간의 방향을 계산
        Vector2 direction = (mousePosition - (Vector2)playerTransform.position).normalized;

        FireBullet(direction);
    }
    void FireBullet(Vector2 direction)
    {
        // 총알을 발사 지점에서 생성
        GameObject bullet = Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 총알에 방향과 속도를 적용
            rb.velocity = direction * bulletSpeed;
        }
    }
}

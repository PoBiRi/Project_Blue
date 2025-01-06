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
        // ������ ������ ���� (-1���� 1 ����)
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // ������ ���� ���� ����
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // ���� �������� �Ѿ� �߻�
        FireBullet(randomDirection);
    }
    void FireBullet(Vector2 direction)
    {
        // �Ѿ��� �߻� �������� ����
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // �Ѿ˿� ����� �ӵ��� ����
            rb.velocity = direction * bulletSpeed;
        }
    }
}

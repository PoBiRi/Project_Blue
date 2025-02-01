using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject chargedBulletPrefab;
    public Transform playerTransform;
    public float bulletSpeed = 20f;

    private float fireRate = 0.2f; // 총알 발사 간격 (초 단위)
    private float nextFireTime = 0f; // 다음 발사 시간
    private bool isCharging = false;
    private float chargeTimer = 0f;
    public float minChargeTime = 1f;  // 최소 차징 시간
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!MenuManage.isGameStart) return;
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsCharging", true);
            isCharging = true;
            chargeTimer = 0f;
        }
        if (isCharging)
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer >= minChargeTime)
            {
                animator.SetBool("IsCharged", true);
            }
        }
        // 마우스 버튼을 떼면 발사
        if (Input.GetMouseButtonUp(0))
        {
            isCharging = false;
            animator.SetBool("IsCharging", false);
            animator.SetBool("IsCharged", false);

            if (chargeTimer >= minChargeTime)
            {
                ChargedBullet();
            }
        }

        if (!isCharging && Time.time >= nextFireTime)
        {
            ShootBullet();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ChargedBullet()
    {
        PlayerSound.GunSound();
        // Click to pos
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // cal direction
        Vector2 direction = (mousePosition - (Vector2)playerTransform.position).normalized;

        // make bullet
        GameObject bullet = Instantiate(chargedBulletPrefab, playerTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * bulletSpeed;
        }
    }

    void ShootBullet()
    {
        PlayerSound.GunSound();
        // Click to pos
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // cal direction
        Vector2 direction = (mousePosition - (Vector2)playerTransform.position).normalized;

        FireBullet(direction);
    }

    void FireBullet(Vector2 direction)
    {
        // make bullet
        GameObject bullet = Instantiate(bulletPrefab, playerTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * bulletSpeed;
        }
    }
}

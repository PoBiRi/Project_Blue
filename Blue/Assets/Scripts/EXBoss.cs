using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EXBoss : MonoBehaviour, Boss
{
    public GameObject bulletPrefab;
    public Transform gunTransform;
    public float bulletSpeed = 5f;
    public float fireInterval = 0.5f;
    public Slider BossHpBar;

    private float BossHP = 100;
    private bool rageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pattern());
    }
    void Update()
    {
        if (BossHpBar == null) // get HPBar Component
        {
            BossHpBar = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
            BossHpBar.value = BossHP / 100;
        }
    }

    private IEnumerator Pattern()
    {
        while (true) // 무한 반복
        {
            // 랜덤 방향으로 발사
            ShootRandomBullet();

            // 대기 후 다음 발사
            yield return new WaitForSeconds(fireInterval);
        }
    }

    void ShootRandomBullet()
    {
        //random
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // to vec2
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        // fire bullet
        FireBullet(randomDirection);
    }

    void FireBullet(Vector2 direction)
    {
        // create bullet
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * bulletSpeed;
        }
    }

    public void getDamage(float dmg) // get Damage to Boss
    {
        if (rageFlag)
        {
            return;
        }
        BossHP -= dmg;
        if (BossHP <= 0)
        {
            BossHP = 0;
            rageFlag = true;
            fireInterval = 0.1f;
        }
        BossHpBar.value = BossHP / 100;
    }

    private void OnTriggerEnter2D(Collider2D other) // if isRage finalattack on
    {
        if (other.CompareTag("Player") || other.CompareTag("Dashing"))
        {
            if (!rageFlag)
            {
                GameObject.Find("Player").GetComponent<Player>().getDamage(10);
            }
            else
            {
                Time.timeScale = 0f;
                MenuManage.isWin = true;
                MenuManage.isGameOver = true;
                Destroy(gameObject);
                Debug.Log("Finalattack"); 
            }
        }
    }
    public void respawn()
    {
        BossHP = 100;
        rageFlag = false;
        BossHpBar.value = BossHP / 100;
    }
}
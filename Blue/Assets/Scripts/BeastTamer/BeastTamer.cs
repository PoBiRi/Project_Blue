using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class BeastTamer : MonoBehaviour, Boss
{
    public GameObject bulletPrefab;
    public GameObject CloudPrefab;
    public GameObject WingPrefab;
    public GameObject TargetingPrefab;
    public GameObject ShieldPrefab;
    public GameObject Player;
    public Transform gunTransform;
    public Slider BossHpBar;

    private float BossHP = 100;
    private float Pattern1_BulletSpeed = 3f;
    private float Pattern2_Interval = 5f;
    private float Pattern3_Interval = 9f;
    private float Pattern3_BulletSpeed = 7f;
    private float Pattern4_Interval = 11f;
    private float Pattern4_BulletSpeed = 9f;
    private bool rageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Cloud();
        StartCoroutine(Pattern2());
        StartCoroutine(Pattern3());
        StartCoroutine(Pattern4());;
        shield();
    }
    void Update()
    {
        if (BossHpBar == null) // get HPBar Component
        {
            BossHpBar = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
            BossHpBar.value = BossHP / 100;
        }
    }

    private IEnumerator Pattern2()
    {
        while (true) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern2_Interval);

            ShotGun();
        }
    }
    private IEnumerator Pattern3()
    {
        while (true) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern3_Interval);

            WingBullet();
        }
    }

    private IEnumerator Pattern4()
    {
        while (true) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern4_Interval);

            TargetingBullet();
        }
    }

    //For Pattern1
    void Cloud()
    {
        //random
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);

        // to vec2
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;

        GameObject bullet = Instantiate(CloudPrefab, gunTransform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = randomDirection * Pattern1_BulletSpeed;
        }
    }

    //For Pattern2
    void ShotGun()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        int bulletCount = 40;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = (Vector2)Player.transform.position + Random.insideUnitCircle * 1.4f;
            // create bullet
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * Random.Range(5f, 10f);
            }
        }
    }

    //For Pattern3
    void WingBullet()
    {
        for(int i = 0; i < 2; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector2 spawn = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * 7;
            Vector2 direction = Random.insideUnitCircle * 5;

            GameObject bullet = Instantiate(WingPrefab, spawn, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = (direction - spawn).normalized * Pattern3_BulletSpeed;
            }
        }
    }

    //For Pattern4
    void TargetingBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        float angleStep = 360f / 6;
        for (int i = 0; i < 6; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            // create bullet
            GameObject bullet = Instantiate(TargetingPrefab, gunTransform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * Pattern4_BulletSpeed;
            }
        }
    }

    //For Pattern5
    void shield ()
    {
        float angleStep = 360f / 6;
        for (int i = 0; i < 6; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = gunTransform.transform.position.normalized;
            float toAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector2 spawn = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)).normalized * 2f;
            // create bullet
            Instantiate(ShieldPrefab, spawn, Quaternion.Euler(new Vector3(0, 0, toAngle)));
        }
    }

    public void getDamage(int dmg) // get Damage to Boss
    {
        if (rageFlag)
        {
            return;
        }
        BossHP -= dmg;
        if (BossHP <= 0)
        {
            if (Player == null)
            {
                Player = GameObject.Find("Player");
            }
            Player.GetComponent<Player>().ragingPush();

            BossHP = 0;
            rageFlag = true;
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
                GameObject.Find("EventSystem").GetComponent<MenuManage>().isWin = true;
                GameObject.Find("EventSystem").GetComponent<MenuManage>().isGameOver = true;
                Destroy(gameObject);
                Debug.Log("Finalattack");
            }
        }
    }

    public void respawn()
    {
        BossHP = 100;
        rageFlag = false;
        if (BossHpBar == null) // get HPBar Component
        {
            BossHpBar = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
        }
        BossHpBar.value = BossHP / 100;
    }
}

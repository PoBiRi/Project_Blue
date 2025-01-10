using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BulletMan : MonoBehaviour, Boss
{
    public GameObject bulletPrefab;
    public GameObject bulletPrefab_Changed;
    public GameObject bombPrefab;
    public GameObject moonPrefab;
    public GameObject Player;
    public Transform gunTransform;
    public Slider BossHpBar;

    private float BossHP = 100;
    private float Pattern1_Interval = 2f;
    private float Pattern1_BulletSpeed = 5f;
    private float Pattern2_Interval = 11f;
    private float Pattern2_BulletSpeed = 3f;
    private float Pattern3_Interval = 9f;
    private float Pattern3_BulletSpeed = 8f;
    private float Pattern4_Interval = 7f;
    private float Pattern4_BulletSpeed = 3f;
    private float Pattern5_Interval = 17f;
    private bool rageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Pattern1());
        StartCoroutine(Pattern2());
        StartCoroutine(Pattern3());
        StartCoroutine(Pattern4());
        StartCoroutine(Pattern5());
    }
    void Update()
    {
        if (BossHpBar == null) // get HPBar Component
        {
            BossHpBar = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
            BossHpBar.value = BossHP / 100;
        }
    }

    private IEnumerator Pattern1()
    {
        bool flag = false;
        while (true) // 무한 반복
        {
            FireBulletCircle(flag, !rageFlag ? 12 : 60);

            yield return new WaitForSeconds(Pattern1_Interval);
            if (flag) { flag = false; }
            else { flag = true; }
        }
    }

    private IEnumerator Pattern2()
    {
        while (true) // 무한 반복
        {
            for (int i = 0; i < (rageFlag ? 12 : 7); i++)
            {
                SlowBullet();
            }

            yield return new WaitForSeconds(rageFlag ? Pattern2_Interval - 7f : Pattern2_Interval);
        }
    }
    private IEnumerator Pattern3()
    {
        while (!rageFlag) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern3_Interval);

            MoonBullet();
        }
    }
    private IEnumerator Pattern4()
    {
        while (!rageFlag) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern4_Interval);

            RingBullet();
        }
    }
    private IEnumerator Pattern5()
    {
        while (true) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern5_Interval);
            StartCoroutine(BurstBullet());
        }
    }

    //For Pattern1
    void FireBulletCircle(bool flag, int bulletCount)
    {
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float size = rageFlag ? 0.5f : 0.1f;
            float angle = flag ? angleStep * i : angleStep * i + 15;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            // create bullet
            GameObject bullet = Instantiate(rageFlag ? bulletPrefab_Changed : bulletPrefab, gunTransform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.transform.localScale = new Vector3(size, size, size);

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * Pattern1_BulletSpeed;
            }
        }
    }

    //For Pattern2
    void SlowBullet()
    {
        float radius = 9.5f;
        // 랜덤 각도 생성 (0 ~ 360도 범위)
        float randomAngle = Random.Range(0f, 360f);
        Vector2 direction = Random.insideUnitCircle * 5;
        Vector2 spawnPosition = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * radius;

        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = (direction - spawnPosition).normalized * Pattern2_BulletSpeed;
        }
    }

    //For Pattern3
    void MoonBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        // create bullet
        Vector2 direction = Player.transform.position.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject moon = Instantiate(moonPrefab, gunTransform.position, Quaternion.Euler(new Vector3(0,0, angle)));
        Rigidbody2D rb = moon.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction * Pattern3_BulletSpeed;
        }
    }

    //For Pattern4
    void RingBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        int bulletCount = 20;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;
            Vector2 spawnPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * 1f, Mathf.Sin(Mathf.Deg2Rad * angle) * 1f);
            Vector2 direction = new Vector2(Player.transform.position.x + Mathf.Cos(Mathf.Deg2Rad * angle) * 1f, Player.transform.position.y + Mathf.Sin(Mathf.Deg2Rad * angle) * 1f);

            // create bullet
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = (direction - spawnPosition).normalized * Pattern4_BulletSpeed;
            }
        }
    }

    //For Pattern5
    IEnumerator BurstBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        int bulletCount = rageFlag ? 60 : 30;
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = (Vector2)Player.transform.position + Random.insideUnitCircle * 1.2f;
            // create bullet
            GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * Random.Range(8f, 10f);
            }
            yield return new WaitForSeconds(0.1f);
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
    }
}
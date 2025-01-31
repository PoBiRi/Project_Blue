using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class BeastTamer : MonoBehaviour, Boss
{
    public GameObject ShotgunPrefab;
    public GameObject CloudPrefab;
    public GameObject WingPrefab;
    public GameObject ZigZagPrefab;
    public GameObject ShieldPrefab;
    public GameObject Player;
    public Transform gunTransform;
    public Slider BossHpBar;
    public GameObject Platform;
    public bool rageFlag = false;

    private float BossHP = 100;
    private float Pattern1_BulletSpeed = 4f;
    private float Pattern2_Interval = 7f;
    private float Pattern3_Interval = 5f;
    private float Pattern3_BulletSpeed = 7f;
    private float Pattern4_Interval = 2f;
    private float Pattern4_BulletSpeed = 7f;
    private int maleAttack = 0;
    private Animator animator;
    private bool isMaleAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("startPattern", 0.5f);
        Platform = GameObject.Find("Platform");
    }
    void Update()
    {
        if (BossHpBar == null) // get HPBar Component
        {
            BossHpBar = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
            BossHpBar.value = BossHP / 100;
        }
    }
    void startPattern()
    {
        Cloud();
        StartCoroutine(Pattern2());
        StartCoroutine(Pattern3());
        StartCoroutine(Pattern4());
        shield();
    }

    private IEnumerator Pattern2()
    {
        while (!rageFlag) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern2_Interval);

            ShotGun();
        }
    }
    private IEnumerator Pattern3()
    {
        while (true) // 무한 반복
        {
            WingBullet();

            yield return new WaitForSeconds(rageFlag ? Pattern3_Interval - 3f : Pattern3_Interval);
        }
    }

    private IEnumerator Pattern4()
    {
        while (!rageFlag) // 무한 반복
        {
            yield return new WaitForSeconds(Pattern4_Interval);

            ZigZagBullet();
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
            float distance = Vector2.Distance(direction, Vector2.zero);
            direction = direction + new Vector2(-direction.y, direction.x).normalized * distance * getTan(15);
            GameObject bullet = Instantiate(ShotgunPrefab, gunTransform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * Random.Range(9f, 12f);
            }
        }
    }

    //For Pattern3
    void WingBullet()
    {
        for(int i = 0; i < 2; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            Vector2 spawn = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * 9;
            Vector2 direction = Random.insideUnitCircle * 5;

            GameObject bullet = Instantiate(WingPrefab, spawn, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<BeastTamer_Wing>().isRage = rageFlag;

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = (direction - spawn).normalized * Pattern3_BulletSpeed;
            }
        }
    }

    //For Pattern4
    void ZigZagBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        float angleStep = 360f / 8;
        for (int i = 0; i < 8; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle));
            // create bullet
            GameObject bullet = Instantiate(ZigZagPrefab, gunTransform.position, Quaternion.identity);
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
            GameObject shield = Instantiate(ShieldPrefab, spawn, Quaternion.Euler(new Vector3(0, 0, toAngle - 90)));
            shield.GetComponent<BeastTamer_Shield>().isRage = rageFlag;
        }
    }

    public void getDamage(int dmg) // get Damage to Boss
    {
        if (rageFlag)
        {
            return;
        }
        BossAndOstSounds.HitSound();
        BossHP -= dmg;
        if (BossHP <= 0)
        {
            if (Player == null)
            {
                Player = GameObject.Find("Player");
            }
            Player.GetComponent<Player>().ragingPush();

            BossAndOstSounds.RageSound();
            BossHP = 0;
            rageFlag = true;
            animator.SetTrigger("Rage");
            Platform.GetComponent<Circle>().ChangeRotationBeastTamer();
            StopAllCoroutines();
            deleteBullet();
            Invoke("stopCoritines", 1f);
        }
        else animator.SetTrigger("Damage");
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
            else if(!isMaleAttacked)
            {
                PlayerSound.MaleAttack();
                isMaleAttacked = true;
                if (maleAttack == 2)
                {
                    animator.SetBool("Defeat", true);
                    Time.timeScale = 0f;
                    MenuManage.isWin = true;
                    MenuManage.isGameOver = true;
                    //Destroy(gameObject);
                    Debug.Log("Finalattack");
                }
                else
                {
                    BossAndOstSounds.RageSound();
                    animator.SetTrigger("Rage");
                    Player.GetComponent<Player>().ragingPush();
                    maleAttack++;
                    StopAllCoroutines();
                    deleteBullet();
                    Invoke("stopCoritines", 1f);
                    Platform.GetComponent<Circle>().ChangeRotationBeastTamer();
                }
            }
        }
    }
    private float getTan(float degree)
    {
        float angleRadians = degree * Mathf.Deg2Rad;
        float tangentValue = Mathf.Tan(angleRadians);
        return tangentValue;
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

    private void stopCoritines()
    {
        isMaleAttacked = false;
        StartCoroutine(Pattern3());
        shield();
    }

    void deleteBullet()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // 오브젝트 삭제
            Debug.Log(obj.name + " 삭제됨");
        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // 오브젝트 삭제
            Debug.Log(obj.name + " 삭제됨");
        }
    }
}

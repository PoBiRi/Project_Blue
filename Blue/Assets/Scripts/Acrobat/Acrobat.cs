using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Acrobat : MonoBehaviour, Boss
{
    public GameObject acroPrefab;
    public GameObject straightPrefab;
    public GameObject ringPrefab;
    public GameObject slowMoonPrefab;
    public GameObject laserPrefab;
    public GameObject Player;
    public Transform gunTransform;
    public Slider BossHpBar;
    public GameObject Platform;

    private float BossHP = 100;
    private float Pattern1_Interval = 4f;
    private float Pattern2_Interval = 7f;
    private float Pattern2_BulletSpeed = 2f;
    private float Pattern3_Interval = 5f;
    private float Pattern4_Interval = 1f;
    private bool rageFlag = false;
    private int maleAttack = 0;
    private Animator animator;
    private bool isMaleAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Platform = GameObject.Find("Platform");
        Invoke("startPattern", 0.1f);
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
        StartCoroutine(Pattern1());
        StartCoroutine(Pattern2());
        StartCoroutine(Pattern3());
        StartCoroutine(Pattern4());
        Laser();
    }

    private IEnumerator Pattern1()
    {
        while (true) // ���� �ݺ�
        {
            StartCoroutine(acroBullet());

            yield return new WaitForSeconds(Pattern1_Interval);
        }
    }

    private IEnumerator Pattern2()
    {
        while (true) // ���� �ݺ�
        {
            yield return new WaitForSeconds(Pattern2_Interval);

            StraightBullet();
        }
    }

    private IEnumerator Pattern3()
    {
        while (true) // ���� �ݺ�
        {
            yield return new WaitForSeconds(Pattern3_Interval);
            for(int i = 0; i < 5; i++) RingBullet();
        }
    }

    private IEnumerator Pattern4()
    {
        while (true) // ���� �ݺ�
        {
            yield return new WaitForSeconds(Pattern4_Interval);

            SlowMoon();
        }
    }

    //For Pattern1
    IEnumerator acroBullet()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        for (int i = 0; i < 8; i++)
        {
            // ���� �Ѿ��� ���� ���

            // �Ѿ� ����
            Instantiate(acroPrefab, (Vector2)gunTransform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

    }

    //For Pattern2
    void StraightBullet()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // ���� ����
        Vector2 spawn = new Vector2(Mathf.Cos(angle) * 10f, Mathf.Sin(angle) * 10f);
        angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle) * 5f, Mathf.Sin(angle) * 5f) - spawn;

        GameObject bullet = Instantiate(straightPrefab, spawn, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        angle = Mathf.Atan2(direction.y, direction.x);
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * Mathf.Rad2Deg));


        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = direction.normalized * Pattern2_BulletSpeed;
        }
        bullet = Instantiate(straightPrefab, -spawn, Quaternion.identity);
        rb = bullet.GetComponent<Rigidbody2D>();
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle * Mathf.Rad2Deg));

        if (rb != null)
        {
            // apply speed and direction
            rb.velocity = -direction.normalized * Pattern2_BulletSpeed;
        }
    }

    //For Pattern3
    void RingBullet()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // ���� ����
        Vector2 spawn = new Vector2(Mathf.Cos(angle) * 11f, Mathf.Sin(angle) * 11f);

        // �Ѿ� ����
        Instantiate(ringPrefab, spawn, Quaternion.identity);
    }

    //For Pattern4
    void SlowMoon()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }

        Vector2 playerDirection = Player.transform.position;
        float startAngle = -45 / 2f; // �ݿ��� ���� ����
        float angleStep = 45 / (8 - 1); // �� �Ѿ� ������ ���� ����

        for (int i = 0; i < 8; i++)
        {
            // ���� �Ѿ��� ���� ���
            float currentAngle = startAngle + (i * angleStep);
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * playerDirection;

            // �Ѿ� ����
            Instantiate(slowMoonPrefab, (Vector2)gunTransform.position + direction.normalized * 0.6f, Quaternion.identity);
        }
    }

    //For Pattern5
    void Laser()
    {
        float angleStep = 360f / 4;
        for (int i = 0; i < 4; i++)
        {
            float angle = angleStep * i + 45;
            Vector2 direction = gunTransform.transform.position.normalized;
            float toAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector2 spawn = new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle)).normalized * 5f;
            // create bullet
            Instantiate(laserPrefab, spawn, Quaternion.Euler(new Vector3(0, 0, toAngle)));
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
            animator.SetTrigger("Rage");
            Platform.GetComponent<Circle>().ChangeRotation(-50f);
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
                    deleteBullet();
                    animator.SetTrigger("Rage");
                    Player.GetComponent<Player>().ragingPush();
                    maleAttack++;
                    Invoke("stopCoritines", 0.2f);
                }
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
    private void stopCoritines()
    {
        isMaleAttacked = false;
        StopAllCoroutines();
        Laser();
    }
    void deleteBullet()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // ������Ʈ ����
            Debug.Log(obj.name + " ������");
        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // ������Ʈ ����
            Debug.Log(obj.name + " ������");
        }
    }
}

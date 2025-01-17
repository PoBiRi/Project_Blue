using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_AcroBullet : EnemyBullet
{
    private int destroyFlag = 0;
    public GameObject bulletPrefab;
    private float attractionForce; // �������� �ӵ�
    private float angle;
    private float distance;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Vector2 attractDirection;
    private float speed = 3f;
    private Vector2 spot; // �߽� ����
    private bool flag = false;

    void Start()
    {
        float tmp = Random.Range(3, 4.8f);
        angle = Random.Range(0, 360) * Mathf.Deg2Rad; // ���� ����
        spot = new Vector2(Mathf.Cos(angle) * tmp, Mathf.Sin(angle) * tmp);
        distance = Vector2.Distance((Vector2)transform.position, spot);
        float crossProduct = ((Vector2)transform.position).x * spot.y - ((Vector2)transform.position).y * spot.x;
        if (crossProduct > 0) flag = true;
        rb = GetComponent<Rigidbody2D>();
        attractionForce = rb.mass * Mathf.Pow(speed, 2) / distance;
    }

    void Update()
    {
        attractDirection = (spot - (Vector2)transform.position).normalized;
        if (flag) direction = new Vector2(-attractDirection.y, attractDirection.x);
        else direction = new Vector2(attractDirection.y, -attractDirection.x);

        rb.velocity = direction * (speed) + (attractDirection * attractionForce * Time.deltaTime);
        // ��ü�� ��ǥ ��ġ�� �̵�
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // meet bullet collapse
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            OutBullet();
        }

        if (other.CompareTag("Enemy"))
        {
            if (destroyFlag > 2)
            {
                OutBullet();
            }
            else destroyFlag++;
        }

        if (other.CompareTag("Player")) // meet player
        {
            Destroy(gameObject);

            GameObject.Find("Player").GetComponent<Player>().getDamage(3);
        }
    }
}

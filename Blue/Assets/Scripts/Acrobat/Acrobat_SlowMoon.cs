using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_SlowMoon : EnemyBullet
{
    public GameObject Platform;
    private Transform donutCenter; // ������ �߽���
    private float donutRadius = 10f; // ������ �׵θ� ������
    public float attractionSpeed = 1f; // �������� �ӵ�
    private Rigidbody2D rb;
    void Start()
    {
        if (Platform == null)
        {
            Platform = GameObject.Find("Platform");
            donutCenter = Platform.transform;
        }
        transform.SetParent(Platform.transform);
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
    void Update()
    {
        if (donutCenter == null)
            return;

        // ���� ��ü�� ���� �߽� ������ ����
        Vector3 directionToCenter = transform.position - donutCenter.position;

        // ���� �׵θ� ��ġ ���
        Vector3 directionNormalized = directionToCenter.normalized;
        Vector3 targetPositionOnDonut = donutCenter.position + directionNormalized * donutRadius;

        // ��ǥ ��ġ������ ���� ���
        Vector3 directionToTarget = targetPositionOnDonut - transform.position;

        // ��ü�� ��ǥ ��ġ�� �̵�
        transform.position += directionToTarget.normalized * attractionSpeed * Time.deltaTime;
        Vector2 direction = directionToTarget;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}

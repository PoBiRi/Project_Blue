using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_SlowMoon : EnemyBullet
{
    public GameObject Platform;
    private Transform donutCenter; // 도넛의 중심점
    private float donutRadius = 10f; // 도넛의 테두리 반지름
    public float attractionSpeed = 1f; // 끌려가는 속도
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

        // 현재 물체와 도넛 중심 사이의 벡터
        Vector3 directionToCenter = transform.position - donutCenter.position;

        // 도넛 테두리 위치 계산
        Vector3 directionNormalized = directionToCenter.normalized;
        Vector3 targetPositionOnDonut = donutCenter.position + directionNormalized * donutRadius;

        // 목표 위치까지의 벡터 계산
        Vector3 directionToTarget = targetPositionOnDonut - transform.position;

        // 물체를 목표 위치로 이동
        transform.position += directionToTarget.normalized * attractionSpeed * Time.deltaTime;
        Vector2 direction = directionToTarget;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}

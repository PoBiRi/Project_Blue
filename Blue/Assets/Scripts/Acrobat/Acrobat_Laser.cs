using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_Laser : EnemyBullet
{
    public GameObject Boss;
    public float rotationSpeed = 20f;
    public float radius;  // 보스를 중심으로 하는 원의 반지름
    private float angle = 0f;  // 현재 각도
    public bool flag = false; // 밧줄 상하 반전용

    private void Start()
    {
        if (Boss == null)
        {
            Boss = GameObject.Find("Acrobat(Clone)");
        }
        gameObject.transform.SetParent(Boss.transform);
        Vector3 directionToBoss = transform.position - Boss.transform.position;
        angle = Mathf.Atan2(directionToBoss.y, directionToBoss.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, flag ? angle - 180 : angle));  // 오브젝트 회전
    }

    private void Update()
    {
        // 원을 그리며 이동할 각도 갱신
        angle += rotationSpeed * Time.deltaTime;  // 시간에 따라 각도 증가
        if (angle >= 360f)  // 각도가 360도를 넘으면 다시 0으로 설정
        {
            angle -= 360f;
        }

        // 보스 중심으로 원을 그리며 위치 계산
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius, 0);
        transform.position = Boss.transform.position + offset;  // 보스 위치 + 원형 경로 위치

        Vector3 directionToBoss = Boss.transform.position - transform.position;  // 보스 방향 계산
        float angleToRotate = Mathf.Atan2(directionToBoss.y, directionToBoss.x) * Mathf.Rad2Deg;  // 보스를 마주보는 회전 각도 계산
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, flag ? angleToRotate - 180 : angleToRotate));  // 오브젝트 회전
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            GameObject.Find("Player").GetComponent<Player>().getDamage(3);
        }
    }
}

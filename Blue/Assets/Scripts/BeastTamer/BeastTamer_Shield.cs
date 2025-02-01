using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeastTamer_Shield : EnemyBullet
{
    public GameObject Boss;
    public bool isRage;

    private float rotationSpeed = 40f;
    private float radius = 2f;  // 보스를 중심으로 하는 원의 반지름
    private float angle = 0f;  // 현재 각도
    private SpriteRenderer spriteRenderer;
    private new Collider2D collider2D;
    private int count = 0;

    private void Start()
    {
        if(Boss == null)
        {
            Boss = GameObject.Find("BeastTamer(Clone)");
        }
        gameObject.transform.SetParent(Boss.transform);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        collider2D = gameObject.GetComponent<Collider2D>();
        Vector3 directionToBoss = transform.position - Boss.transform.position;
        angle = Mathf.Atan2(directionToBoss.y, directionToBoss.x) * Mathf.Rad2Deg;
        if (isRage)
        {
            gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
            Destroy(gameObject, 20f);
        }
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate - 180));  // 오브젝트 회전
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet")) // meet bullet collapse
        {
            if (isRage) return;
            if (count > 4)
            {
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
                StartCoroutine(ReactivateAfterDelay(10f));
                count = 0;
            }
            else count++;
        }
    }
    private IEnumerator ReactivateAfterDelay(float delay)
    {
        // 일정 시간 대기
        yield return new WaitForSeconds(delay);

        // SpriteRenderer와 Collider2D를 다시 활성화
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}

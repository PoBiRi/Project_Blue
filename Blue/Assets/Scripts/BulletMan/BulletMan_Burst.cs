using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManBurst : EnemyBullet
{
    /*private Rigidbody2D rb;
    private GameObject Player;
    public float newSpeed = 5f;  // 새로운 속도
    public float delayTime = 20f; // 몇 초 후에 속도 변경

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        StartCoroutine(ChangeSpeedAfterDelay());
    }

    private IEnumerator ChangeSpeedAfterDelay()
    {

        yield return new WaitForSeconds(delayTime);  // 일정 시간 대기
        Debug.Log("hello");

        // 지정된 시간 후에 속도 변경
        Vector2 direction = rb.velocity.normalized;  // 현재 속도의 방향
        rb.velocity = (Vector2)Player.transform.position * newSpeed;  // 새로운 속도와 방향 적용
        //rb.velocity = direction * newSpeed;  // 새로운 속도와 방향 적용
    }*/
}

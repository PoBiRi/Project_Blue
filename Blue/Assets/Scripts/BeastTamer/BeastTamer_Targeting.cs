using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastTamer_Targeting : EnemyBullet
{
    private float speed = 3.5f;           // 총알 이동 속도
    private float followDelay = 0.5f;    // 몇 초 뒤부터 플레이어를 따라갈지 설정
    private bool isFollowing = false; // 플레이어를 따라가고 있는지 여부
    public GameObject Player;

    void Start()
    {
        StartCoroutine(StartFollowingAfterDelay());
        gameObject.transform.SetParent(GameObject.Find("Platform").transform);
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        if (isFollowing)
        {
            if (Player == null)
            {
                Player = GameObject.Find("Player");
            }
            Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position;
            // create bullet
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // apply speed and direction
                rb.velocity = direction.normalized * speed;
            }
        }
    }
    private IEnumerator StartFollowingAfterDelay()
    {
        // followDelay 동안 기다림
        yield return new WaitForSeconds(followDelay);

        // 따라가기 활성화
        isFollowing = true;
    }
}

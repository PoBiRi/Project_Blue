using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastTamer_Targeting : EnemyBullet
{
    private float speed = 3.5f;           // �Ѿ� �̵� �ӵ�
    private float followDelay = 0.5f;    // �� �� �ں��� �÷��̾ ������ ����
    private bool isFollowing = false; // �÷��̾ ���󰡰� �ִ��� ����
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
        // followDelay ���� ��ٸ�
        yield return new WaitForSeconds(followDelay);

        // ���󰡱� Ȱ��ȭ
        isFollowing = true;
    }
}

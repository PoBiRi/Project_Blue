using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManBurst : EnemyBullet
{
    /*private Rigidbody2D rb;
    private GameObject Player;
    public float newSpeed = 5f;  // ���ο� �ӵ�
    public float delayTime = 20f; // �� �� �Ŀ� �ӵ� ����

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

        yield return new WaitForSeconds(delayTime);  // ���� �ð� ���
        Debug.Log("hello");

        // ������ �ð� �Ŀ� �ӵ� ����
        Vector2 direction = rb.velocity.normalized;  // ���� �ӵ��� ����
        rb.velocity = (Vector2)Player.transform.position * newSpeed;  // ���ο� �ӵ��� ���� ����
        //rb.velocity = direction * newSpeed;  // ���ο� �ӵ��� ���� ����
    }*/
}

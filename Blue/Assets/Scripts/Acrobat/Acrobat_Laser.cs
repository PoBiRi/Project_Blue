using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_Laser : EnemyBullet
{
    public GameObject Boss;
    private float rotationSpeed = 20f;
    private float radius = 5f;  // ������ �߽����� �ϴ� ���� ������
    private float angle = 0f;  // ���� ����

    private void Start()
    {
        if (Boss == null)
        {
            Boss = GameObject.Find("Acrobat(Clone)");
        }
        gameObject.transform.SetParent(Boss.transform);
        Vector3 directionToBoss = transform.position - Boss.transform.position;
        angle = Mathf.Atan2(directionToBoss.y, directionToBoss.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        // ���� �׸��� �̵��� ���� ����
        angle += rotationSpeed * Time.deltaTime;  // �ð��� ���� ���� ����
        if (angle >= 360f)  // ������ 360���� ������ �ٽ� 0���� ����
        {
            angle -= 360f;
        }

        // ���� �߽����� ���� �׸��� ��ġ ���
        Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sin(angle * Mathf.Deg2Rad) * radius, 0);
        transform.position = Boss.transform.position + offset;  // ���� ��ġ + ���� ��� ��ġ

        Vector3 directionToBoss = Boss.transform.position - transform.position;  // ���� ���� ���
        float angleToRotate = Mathf.Atan2(directionToBoss.y, directionToBoss.x) * Mathf.Rad2Deg;  // ������ ���ֺ��� ȸ�� ���� ���
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate));  // ������Ʈ ȸ��
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // meet player
        {
            GameObject.Find("Player").GetComponent<Player>().getDamage(3);
        }
    }
}

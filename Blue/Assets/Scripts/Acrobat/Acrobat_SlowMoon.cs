using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acrobat_SlowMoon : EnemyBullet
{
    public GameObject Platform;
    private Transform donutCenter; // ������ �߽���
    private float donutRadius =10f; // ������ �׵θ� ������
    private float attractionSpeed = 1f; // �������� �ӵ�
    void Start()
    {
        if (Platform == null)
        {
            Platform = GameObject.Find("Platform");
            donutCenter = Platform.transform;
        }
        transform.SetParent(Platform.transform);
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
    }
}

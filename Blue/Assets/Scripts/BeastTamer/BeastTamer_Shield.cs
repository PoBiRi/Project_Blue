using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeastTamer_Shield : EnemyBullet
{
    public GameObject Boss;
    public bool isRage;

    private float rotationSpeed = 40f;
    private float radius = 2f;  // ������ �߽����� �ϴ� ���� ������
    private float angle = 0f;  // ���� ����
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate - 180));  // ������Ʈ ȸ��
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
        // ���� �ð� ���
        yield return new WaitForSeconds(delay);

        // SpriteRenderer�� Collider2D�� �ٽ� Ȱ��ȭ
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}

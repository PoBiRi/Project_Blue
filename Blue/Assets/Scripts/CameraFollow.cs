using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform
    public float smoothSpeed = 1f;  // ī�޶� �̵� �ε巴�� �ϴ� �ӵ�
    public Vector3 offset;  // ī�޶�� �÷��̾� ���� ��ġ ����

    public Vector2 minBounds;  // ���� �ּ� ��� (�ּ� x, �ּ� y)
    public Vector2 maxBounds;  // ���� �ִ� ��� (�ִ� x, �ִ� y)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // �÷��̾��� ���� ��ġ�� ī�޶��� �������� ���� ��ǥ ��ġ ���
        Vector3 desiredPosition = player.position + offset;

        // ī�޶� �� ������ ������ �ʵ��� ����
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // ���ѵ� ��ġ�� ī�޶� �ε巴�� �̵�
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        //transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class CameraFollow : MonoBehaviour
{
    public GameObject Map;
    public Transform player;  // �÷��̾��� Transform
    public float smoothSpeed = 1f;  // ī�޶� �̵� �ε巴�� �ϴ� �ӵ�
    public Vector3 offset;  // ī�޶�� �÷��̾� ���� ��ġ ����

    private Vector3 velocity = Vector3.zero;
    private Camera cam;
    private Vector3 bound;

    private void Start()
    {
        cam = Camera.main;
        //Screen.SetResolution(960, 540, false);
    }


    void LateUpdate()
    { 
        bound = Map.GetComponent<Collider2D>().bounds.size;
        float camHeight = cam.orthographicSize;
        float camWidth = cam.aspect * camHeight;
        // �÷��̾��� ���� ��ġ�� ī�޶��� �������� ���� ��ǥ ��ġ ���
        Vector3 desiredPosition = player.position + offset;
        float XBounds = bound.x/2;
        float YBounds = bound.y/2;

        // ī�޶� �� ������ ������ �ʵ��� ����
        float clampedX = Mathf.Clamp(desiredPosition.x, -XBounds + camWidth, XBounds - camWidth);
        float clampedY = Mathf.Clamp(desiredPosition.y, -YBounds + camHeight, YBounds - camHeight);

        // ���ѵ� ��ġ�� ī�޶� �ε巴�� �̵�
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        //transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothSpeed);
    }
}

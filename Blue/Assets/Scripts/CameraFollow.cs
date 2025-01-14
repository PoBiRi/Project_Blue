using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class CameraFollow : MonoBehaviour
{
    public GameObject Map;
    public Transform player;  // 플레이어의 Transform
    public float smoothSpeed = 1f;  // 카메라 이동 부드럽게 하는 속도
    public Vector3 offset;  // 카메라와 플레이어 간의 위치 차이

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
        // 플레이어의 현재 위치에 카메라의 오프셋을 더한 목표 위치 계산
        Vector3 desiredPosition = player.position + offset;
        float XBounds = bound.x/2;
        float YBounds = bound.y/2;

        // 카메라가 맵 밖으로 나가지 않도록 제한
        float clampedX = Mathf.Clamp(desiredPosition.x, -XBounds + camWidth, XBounds - camWidth);
        float clampedY = Mathf.Clamp(desiredPosition.y, -YBounds + camHeight, YBounds - camHeight);

        // 제한된 위치로 카메라를 부드럽게 이동
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        //transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothSpeed);
    }
}

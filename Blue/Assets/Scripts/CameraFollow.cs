using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform
    public float smoothSpeed = 1f;  // 카메라 이동 부드럽게 하는 속도
    public Vector3 offset;  // 카메라와 플레이어 간의 위치 차이

    public Vector2 minBounds;  // 맵의 최소 경계 (최소 x, 최소 y)
    public Vector2 maxBounds;  // 맵의 최대 경계 (최대 x, 최대 y)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // 플레이어의 현재 위치에 카메라의 오프셋을 더한 목표 위치 계산
        Vector3 desiredPosition = player.position + offset;

        // 카메라가 맵 밖으로 나가지 않도록 제한
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // 제한된 위치로 카메라를 부드럽게 이동
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);
        //transform.position = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);
        transform.position = Vector3.SmoothDamp(transform.position, clampedPosition, ref velocity, smoothSpeed);
    }
}

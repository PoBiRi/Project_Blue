using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private float rotationSpeed = 20f; 

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); //rotate
    }

    public void ChangeRotationBeastTamer()
    {
        rotationSpeed *= -1;
    }

    private IEnumerator ChangeRotationCorutine()
    {
        float minSpeed = 10f;
        float maxSpeed = 30f;
        while (true)
        {
            float targetSpeed = Random.Range(minSpeed, maxSpeed) * (Random.value > 0.5f ? 1 : -1); // 랜덤 속도 및 방향
            float startSpeed = rotationSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < 2f)
            {
                rotationSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / 2f);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rotationSpeed = targetSpeed; // 최종 값 고정
        }
    }
    public void ChangeRotationAcrobat()
    {
        StartCoroutine(ChangeRotationCorutine());
    }

    public void respawn()
    {
        StopAllCoroutines();
        rotationSpeed = 20f;
    }
}

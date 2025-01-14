using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspect : MonoBehaviour
{
    public int targetWidth = 1920;  // 기본 너비
    public int targetHeight = 1080; // 기본 높이
    public bool fullscreen = false; // 전체 화면 모드 설정
    // Start is called before the first frame update
    void Start()
    {
        SetFixedResolution();
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.width != targetWidth || Screen.height != targetHeight)
        {
            SetFixedResolution();
        }
    }
    void SetFixedResolution()
    {
        float targetAspect = 16.0f / 9.0f; // 목표 비율 (16:9)
        float currentAspect = (float)Screen.width / Screen.height;

        if (Mathf.Abs(currentAspect - targetAspect) > 0.01f)
        {
            // 현재 비율과 목표 비율이 다를 경우, 크기 재조정
            if (currentAspect > targetAspect)
            {
                // 현재 창이 목표 비율보다 넓음 (너비 축소)
                targetWidth = Mathf.RoundToInt(Screen.height * targetAspect);
                targetHeight = Screen.height;
            }
            else
            {
                // 현재 창이 목표 비율보다 좁음 (높이 축소)
                targetHeight = Mathf.RoundToInt(Screen.width / targetAspect);
                targetWidth = Screen.width;
            }
        }

        // 화면 크기 적용
        Screen.SetResolution(targetWidth, targetHeight, fullscreen);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspect : MonoBehaviour
{
    public int targetWidth = 1920;  // �⺻ �ʺ�
    public int targetHeight = 1080; // �⺻ ����
    public bool fullscreen = false; // ��ü ȭ�� ��� ����
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
        float targetAspect = 16.0f / 9.0f; // ��ǥ ���� (16:9)
        float currentAspect = (float)Screen.width / Screen.height;

        if (Mathf.Abs(currentAspect - targetAspect) > 0.01f)
        {
            // ���� ������ ��ǥ ������ �ٸ� ���, ũ�� ������
            if (currentAspect > targetAspect)
            {
                // ���� â�� ��ǥ �������� ���� (�ʺ� ���)
                targetWidth = Mathf.RoundToInt(Screen.height * targetAspect);
                targetHeight = Screen.height;
            }
            else
            {
                // ���� â�� ��ǥ �������� ���� (���� ���)
                targetHeight = Mathf.RoundToInt(Screen.width / targetAspect);
                targetWidth = Screen.width;
            }
        }

        // ȭ�� ũ�� ����
        Screen.SetResolution(targetWidth, targetHeight, fullscreen);
    }
}

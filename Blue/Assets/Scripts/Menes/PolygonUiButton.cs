using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PolygonUiButton : MonoBehaviour
{
    void Start()
    {
        Image img = GetComponent<Image>();
        img.alphaHitTestMinimumThreshold = 0.1f; // 0.1 �̻��� ������ Ŭ�� ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    public RectTransform flyer; // ������ UI�� RectTransform
    public float slideDuration = 1f; // �����̵� �ð�
    public GameObject Scripts;
    private Vector2 startBottomPosition; // ���� ��ġ (ȭ�� �Ʒ�)
    private Vector2 centerPosition;     // ȭ�� �߾� ��ġ
    private Vector2 endTopPosition;     // �� ��ġ (ȭ�� ��)
    private bool isFirst = false;

    void Start()
    {
        // �ʱ� ��ġ ����
        startBottomPosition = new Vector2(0, -Screen.height - 250);
        centerPosition = Vector2.zero;
        endTopPosition = new Vector2(0, Screen.height + 250);
        flyer.anchoredPosition = startBottomPosition; // �������� ȭ�� �Ʒ��� ��ġ
        StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, slideDuration));
    }
    private void OnEnable()
    {
        if(isFirst)
        {
            flyer.anchoredPosition = startBottomPosition; // �������� ȭ�� �Ʒ��� ��ġ
                                                          // ���� �ִϸ��̼� ����
            StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, slideDuration));
        }
    }

    public void OnFlyerClicked()
    {
        // Ŭ�� �� ȭ�� ���� �̵�
        StartCoroutine(SlideFlyerUnscaled(flyer, centerPosition, endTopPosition, slideDuration));
    }

    private IEnumerator SlideFlyerUnscaled(RectTransform rect, Vector2 from, Vector2 to, float duration)
    {
        isFirst = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Unscaled Time ���
            rect.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // ����
            yield return null;
        }

        rect.anchoredPosition = to; // ���� ��ġ�� ����

        if(to == Vector2.zero)  GameObject.Find("EventSystem").GetComponent<MenuManage>().spawnBoss();
        else if(to == new Vector2(0, Screen.height + 250))
        {
            gameObject.SetActive(false);
            Scripts.SetActive(true);
        }

    }
}

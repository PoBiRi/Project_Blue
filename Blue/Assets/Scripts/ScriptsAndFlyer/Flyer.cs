using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flyer : MonoBehaviour
{
    public RectTransform flyer; // ������ UI�� RectTransform
    public float slideDuration = 1f; // �����̵� �ð�
    public GameObject Scripts;
    public Sprite[] Sprites;
    private Vector2 startBottomPosition; // ���� ��ġ (ȭ�� �Ʒ�)
    private Vector2 centerPosition;     // ȭ�� �߾� ��ġ
    private Vector2 endTopPosition;     // �� ��ġ (ȭ�� ��)
    private bool isFirst = false;
    private Button Button;
    private Image Image;

    void Start()
    {
        Image = gameObject.GetComponent<Image>();
        Image.sprite = Sprites[MenuManage.BossNum];
        Button = gameObject.GetComponent<Button>();
        // �ʱ� ��ġ ����
        RectTransform parentRect = flyer.parent.GetComponent<RectTransform>();
        float parentHeight = parentRect.rect.height;

        startBottomPosition = new Vector2(0, -parentHeight - 500); // ȭ�� �Ʒ�
        centerPosition = Vector2.zero; // ȭ�� �߾�
        endTopPosition = new Vector2(0, parentHeight + 500); // ȭ�� ��

        float duration = BossAndOstSounds.DrumForFlyers();
        BossAndOstSounds.ClapForFlyers();

        flyer.anchoredPosition = startBottomPosition; // �������� ȭ�� �Ʒ��� ��ġ
        StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, duration));
    }
    private void OnEnable()
    {
        Image = gameObject.GetComponent<Image>();
        Image.sprite = Sprites[MenuManage.BossNum];
        if (isFirst)
        {
            flyer.anchoredPosition = startBottomPosition; // �������� ȭ�� �Ʒ��� ��ġ
                                                          // ���� �ִϸ��̼� ����
            float duration = BossAndOstSounds.DrumForFlyers();
            BossAndOstSounds.ClapForFlyers();
            StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, duration));
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
        Button.interactable = false;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Unscaled Time ���
            rect.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // ����
            yield return null;
        }

        rect.anchoredPosition = to; // ���� ��ġ�� ����
        Button.interactable = true;

        if (to == Vector2.zero) GameObject.Find("EventSystem").GetComponent<MenuManage>().spawnBoss();
        else if (to == endTopPosition)
        {
            gameObject.SetActive(false);
            Scripts.SetActive(true);
        }
    }
}

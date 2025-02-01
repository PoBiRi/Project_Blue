using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flyer : MonoBehaviour
{
    public RectTransform flyer; // 전단지 UI의 RectTransform
    public float slideDuration = 1f; // 슬라이드 시간
    public GameObject Scripts;
    public Sprite[] Sprites;
    private Vector2 startBottomPosition; // 시작 위치 (화면 아래)
    private Vector2 centerPosition;     // 화면 중앙 위치
    private Vector2 endTopPosition;     // 끝 위치 (화면 위)
    private bool isFirst = false;
    private Button Button;
    private Image Image;

    void Start()
    {
        Image = gameObject.GetComponent<Image>();
        Image.sprite = Sprites[MenuManage.BossNum];
        Button = gameObject.GetComponent<Button>();
        // 초기 위치 설정
        RectTransform parentRect = flyer.parent.GetComponent<RectTransform>();
        float parentHeight = parentRect.rect.height;

        startBottomPosition = new Vector2(0, -parentHeight - 500); // 화면 아래
        centerPosition = Vector2.zero; // 화면 중앙
        endTopPosition = new Vector2(0, parentHeight + 500); // 화면 위

        float duration = BossAndOstSounds.DrumForFlyers();
        BossAndOstSounds.ClapForFlyers();

        flyer.anchoredPosition = startBottomPosition; // 전단지를 화면 아래로 배치
        StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, duration));
    }
    private void OnEnable()
    {
        Image = gameObject.GetComponent<Image>();
        Image.sprite = Sprites[MenuManage.BossNum];
        if (isFirst)
        {
            flyer.anchoredPosition = startBottomPosition; // 전단지를 화면 아래로 배치
                                                          // 시작 애니메이션 실행
            float duration = BossAndOstSounds.DrumForFlyers();
            BossAndOstSounds.ClapForFlyers();
            StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, duration));
        }
    }

    public void OnFlyerClicked()
    {
        // 클릭 시 화면 위로 이동
        StartCoroutine(SlideFlyerUnscaled(flyer, centerPosition, endTopPosition, slideDuration));
    }

    private IEnumerator SlideFlyerUnscaled(RectTransform rect, Vector2 from, Vector2 to, float duration)
    {
        isFirst = true;
        Button.interactable = false;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Unscaled Time 사용
            rect.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // 보간
            yield return null;
        }

        rect.anchoredPosition = to; // 최종 위치로 고정
        Button.interactable = true;

        if (to == Vector2.zero) GameObject.Find("EventSystem").GetComponent<MenuManage>().spawnBoss();
        else if (to == endTopPosition)
        {
            gameObject.SetActive(false);
            Scripts.SetActive(true);
        }
    }
}

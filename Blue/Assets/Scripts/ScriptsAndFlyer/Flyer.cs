using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : MonoBehaviour
{
    public RectTransform flyer; // 전단지 UI의 RectTransform
    public float slideDuration = 1f; // 슬라이드 시간
    public GameObject Scripts;
    private Vector2 startBottomPosition; // 시작 위치 (화면 아래)
    private Vector2 centerPosition;     // 화면 중앙 위치
    private Vector2 endTopPosition;     // 끝 위치 (화면 위)
    private bool isFirst = false;

    void Start()
    {
        // 초기 위치 설정
        startBottomPosition = new Vector2(0, -Screen.height - 250);
        centerPosition = Vector2.zero;
        endTopPosition = new Vector2(0, Screen.height + 250);
        flyer.anchoredPosition = startBottomPosition; // 전단지를 화면 아래로 배치
        StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, slideDuration));
    }
    private void OnEnable()
    {
        if(isFirst)
        {
            flyer.anchoredPosition = startBottomPosition; // 전단지를 화면 아래로 배치
                                                          // 시작 애니메이션 실행
            StartCoroutine(SlideFlyerUnscaled(flyer, startBottomPosition, centerPosition, slideDuration));
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
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Unscaled Time 사용
            rect.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // 보간
            yield return null;
        }

        rect.anchoredPosition = to; // 최종 위치로 고정

        if(to == Vector2.zero)  GameObject.Find("EventSystem").GetComponent<MenuManage>().spawnBoss();
        else if(to == new Vector2(0, Screen.height + 250))
        {
            gameObject.SetActive(false);
            Scripts.SetActive(true);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scripts : MonoBehaviour
{
    public GameObject Map;
    public GameObject parent;
    public CanvasGroup canvasGroup;
    private MenuManage main;
    private Image Image;
    private void Start()
    {
        main = GameObject.Find("EventSystem").GetComponent<MenuManage>();
        Image = gameObject.GetComponent<Image>();
        Color color = Image.color;
        color.a = Mathf.Clamp01(0);
    }
    public void onScriptsCliked()
    {
        startGame();
    }
    void startGame()
    {
        Color color = Image.color;
        color.a = Mathf.Clamp01(0);
        Image.color = color;
        StartCoroutine(FadeCanvasGroup(0f, 1f, 2f));
    }
    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration); // 선형 보간
            canvasGroup.alpha = newAlpha;
            yield return null;
        }
        canvasGroup.alpha = endAlpha; // 최종 alpha 값 설정

        Vector3 bound = Map.GetComponent<Collider2D>().bounds.size;
        float YBounds = bound.y / 2;
        float camHeight = Camera.main.orthographicSize;
        while (Camera.main.transform.position.y > -YBounds + camHeight + 0.1f)
        {
            // 현재 위치에서 아래로 이동
            Camera.main.transform.position = new Vector3(
                0,
                transform.position.y - 2.5f * Time.unscaledDeltaTime,
                -10
            );
            yield return null; // 다음 프레임까지 대기
        }

        // 목표 위치 도달 후 위치 고정
        Camera.main.transform.position = new Vector3(0, -YBounds + camHeight + 0.1f, -10);

        main.startTime();
        Color color = Image.color;
        color.a = Mathf.Clamp01(1);
        Image.color = color;
        gameObject.SetActive(false);
        parent.SetActive(false);
        MenuManage.isGameStart = true;
    }
}

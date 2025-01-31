using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scripts : MonoBehaviour
{
    public GameObject Map;
    public GameObject parent;
    public CanvasGroup canvasGroup;
    public TMP_Text Text;
    private MenuManage main;
    private string currentText = "";
    private CanvasGroup scriptGroup;
    private int cliked = 0;

    private string[,] dialogues = new string[3, 2]
    {
        {"Hey, who are you calling short boyo?!", "I betcha wont look down on me when both your knees is shot!"},
        {"Just so you know, I don't see you as an enemy.", "You are prey"},
        {"Is that all combattant?  It's a wonder that you survived this long.", "I don't like getting my hands dirty, I hope you won't mind."}
    };

    private void Start()
    {
        scriptGroup = gameObject.GetComponent<CanvasGroup>();
        main = GameObject.Find("EventSystem").GetComponent<MenuManage>();
    }
    public void onScriptsCliked()
    {
        cliked++;
        if(cliked > 2)
        {
            cliked = 0;
            Text.text = "";
            startGame();
        }
        else
        {
            StartCoroutine(ShowText(dialogues[MenuManage.BossNum, cliked - 1]));
        }
    }
    private IEnumerator ShowText(string fullText)
    {
        scriptGroup.blocksRaycasts = false;
        scriptGroup.interactable = false;
        currentText = "";
        Text.text = currentText;

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            Text.text = currentText;
            float duration = BossAndOstSounds.Scripts();
            yield return new WaitForSecondsRealtime(0.05f);
        }
        scriptGroup.blocksRaycasts = true;
        scriptGroup.interactable = true;
    }
    void startGame()
    {
        scriptGroup.alpha = 0;
        StartCoroutine(FadeCanvasGroup(0f, 1f, 2f));
    }
    private IEnumerator FadeCanvasGroup(float startAlpha, float endAlpha, float duration)
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        scriptGroup.blocksRaycasts = false;
        scriptGroup.interactable = false;

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
        while (Camera.main.transform.position.y > -YBounds + camHeight)
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
        Camera.main.transform.position = new Vector3(0, -YBounds + camHeight, -10);

        main.startTime();
        scriptGroup.alpha = 1f;
        scriptGroup.interactable = true;
        scriptGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        gameObject.SetActive(false);
        parent.SetActive(false);
        MenuManage.isGameStart = true;
    }
}

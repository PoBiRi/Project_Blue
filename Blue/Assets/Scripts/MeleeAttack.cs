using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private CanvasGroup CanvasGroup;
    private void Start()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void InfoMelee()
    {
        StartCoroutine(FadeOutCanvas());
    }
    private IEnumerator FadeOutCanvas()
    {
        // 1. 알파 값을 1로 설정하고 유지
        CanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(2f);

        // 2. 서서히 알파 값을 0으로 변경
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / 1f);
            yield return null;
        }

        // 3. 최종적으로 알파 값 0 설정
        CanvasGroup.alpha = 0f;
    }
}

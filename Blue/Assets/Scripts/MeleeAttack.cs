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
        // 1. ���� ���� 1�� �����ϰ� ����
        CanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(2f);

        // 2. ������ ���� ���� 0���� ����
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            CanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / 1f);
            yield return null;
        }

        // 3. ���������� ���� �� 0 ����
        CanvasGroup.alpha = 0f;
    }
}

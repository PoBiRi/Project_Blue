using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

public class MenuManage : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject optionMenu;
    public GameObject ScriptsAndFlyer;
    public GameObject Flyer;
    public GameObject gameOverDefeatMenu;
    public CanvasGroup gameOverDefeatMenues;
    public GameObject gameOverWinMenu;
    public RectTransform Cane;
    public GameObject gameObj;
    public CanvasGroup HP;
    public GameObject[] Boss; // ������ ������

    public bool isWin = false;
    public bool isGameOver = false;
    public bool isGameStart = false;

    private bool isESC = false;
    public int BossNum = 0;
    private bool isDefeatFlag = false;
    private bool isWinFlag = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStart)
        { 
            if(!isESC)
            {
                stopTime(); // ���� ����
                optionMenu.SetActive(true);
                isESC = true;
            }
            else
            {
                startTime();
                optionMenu.SetActive(false);
                controlsMenu.SetActive(false);
                isESC = false;
            }
        }
        if (isGameOver)
        {
            stopTime();
            //gameObj.SetActive(false);
            if(isWin)
            {
                if (!isWinFlag)
                {
                    isWinFlag = true;
                    StartCoroutine(gameoverWinAni(2f));
                }
            }
            else
            {
                if (!isDefeatFlag)
                {
                    isDefeatFlag = true;
                    StartCoroutine(gameoverDefeatAni(0f, 1f, 2f));
                }
            }
        }
    }

    public void stopTime()
    {
        Time.timeScale = 0f; // ���� ����
    }
    public void startTime()
    {
        Time.timeScale = 1f; 
    }

    public void main_gameStart()
    {
        isGameStart = false;
        HP.alpha = 0f;
        gameObj.SetActive(true);
        mainMenu.SetActive(false);
        GameObject.Find("Platform").GetComponent<Circle>().respawn();
        GameObject.Find("Player").GetComponent<Player>().respawn();
        stopTime();
        Camera.main.transform.position = new Vector3(0, -2f, -10f);
        ScriptsAndFlyer.gameObject.SetActive(true);
        Flyer.gameObject.SetActive(true);
    }

    public void toMain()
    {
        mainMenu.SetActive(true);
        gameObj.SetActive(false);
        optionMenu.SetActive(false);
        gameOverDefeatMenu.SetActive(false);
        gameOverWinMenu.SetActive(false);
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Enemy");
        Destroy(tmp[0]);
        deleteBullet();
        BossNum = 0;
    }
    
    public void spawnBoss()
    {
        Instantiate(Boss[BossNum]);
    }

    public void main_EXIT()
    {
        Application.Quit();
    }

    public void controlsMenu_On()
    {
        controlsMenu.SetActive(true);
    }

    public void controlsMenu_Off()
    {
        controlsMenu.SetActive(false);
    }

    public void option_returnToGame()
    {
        startTime(); 
        optionMenu.SetActive(false);
        isESC = false;
    }


    private IEnumerator gameoverDefeatAni(float startAlpha, float endAlpha, float duration)
    {
        gameOverDefeatMenues.alpha = 0;
        gameOverDefeatMenues.interactable = false;
        Color finalColor = gameOverDefeatMenu.GetComponent<Image>().color;
        finalColor.a = 0;
        gameOverDefeatMenu.GetComponent<Image>().color = finalColor;

        gameOverDefeatMenu.SetActive(true);
        float elapsedTime = 0f;

        // ���� alpha �� ����
        Color startColor = gameOverDefeatMenu.GetComponent<Image>().color;
        startColor.a = startAlpha;  // ���� alpha ������ ����
        gameOverDefeatMenu.GetComponent<Image>().color = startColor; // �ʱ� ������ ���� ����

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            // ���� ������ ����Ͽ� alpha �� ���
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);

            // �̹����� alpha ���� ����
            Color newColor = gameOverDefeatMenu.GetComponent<Image>().color;
            newColor.a = newAlpha;
            gameOverDefeatMenu.GetComponent<Image>().color = newColor;

            yield return null;
        }

        // ���� alpha �� ����
        finalColor = gameOverDefeatMenu.GetComponent<Image>().color;
        finalColor.a = endAlpha;
        gameOverDefeatMenu.GetComponent<Image>().color = finalColor;

        deleteBullet();

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration); // ���� ����
            gameOverDefeatMenues.alpha = newAlpha;
            yield return null;
        }
        gameOverDefeatMenues.alpha = endAlpha; // ���� alpha �� ����
        gameOverDefeatMenues.interactable = true;
    }

    public void gameoverDefeat_restartGame()
    {
        isGameOver = false;
        isDefeatFlag = false;
        gameOverDefeatMenu.SetActive(false);

        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Enemy");
        Destroy(tmp[0]);
        main_gameStart();
    }

    private IEnumerator gameoverWinAni(float duration)
    {
        isGameStart = false;
        gameOverWinMenu.SetActive(true);
        float elapsedTime = 0f;

        // ���� alpha �� ����
        Color startColor = gameOverWinMenu.GetComponent<Image>().color;
        startColor.a = 0;  // ���� alpha ������ ����
        gameOverWinMenu.GetComponent<Image>().color = startColor; // �ʱ� ������ ���� ����

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            // ���� ������ ����Ͽ� alpha �� ���
            float newAlpha = Mathf.Lerp(0, 1f, elapsedTime / duration);

            // �̹����� alpha ���� ����
            Color newColor = gameOverWinMenu.GetComponent<Image>().color;
            newColor.a = newAlpha;
            gameOverWinMenu.GetComponent<Image>().color = newColor;

            yield return null;
        }

        deleteBullet();
        HP.alpha = 0f;
        GameObject.Find("Player").GetComponent<Player>().respawn();
        Camera.main.transform.position = new Vector3(0, -2f, -10f);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;

            // ���� ������ ����Ͽ� alpha �� ���
            float newAlpha = Mathf.Lerp(1, 0, elapsedTime / duration);

            // �̹����� alpha ���� ����
            Color newColor = gameOverWinMenu.GetComponent<Image>().color;
            newColor.a = newAlpha;
            gameOverWinMenu.GetComponent<Image>().color = newColor;

            yield return null;
        }

        Vector2 initialPosition = Cane.anchoredPosition;
        Vector2 downPosition = initialPosition + new Vector2(0, -450f);
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Cane.anchoredPosition = Vector2.Lerp(initialPosition, downPosition, elapsedTime / duration);
            yield return null;
        }
        Cane.anchoredPosition = downPosition;

        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Enemy");
        tmp[0].transform.SetParent(Cane.transform);

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            Cane.anchoredPosition = Vector2.Lerp(downPosition, initialPosition, elapsedTime / duration);
            yield return null;
        }
        Cane.anchoredPosition = initialPosition;

        BossNum++;
        Destroy(tmp[0]);
        isGameOver = false;
        isWinFlag = false;
        gameOverWinMenu.SetActive(false);
        main_gameStart();
    }

        void deleteBullet()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // ������Ʈ ����
            Debug.Log(obj.name + " ������");
        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // ������Ʈ ����
            Debug.Log(obj.name + " ������");
        }
    }
}

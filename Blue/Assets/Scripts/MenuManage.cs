using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UI;

public class MenuManage : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionMenu;
    public GameObject gameOverDefeatMenu;
    public GameObject gameOverWinMenu;
    public GameObject gameObj;
    public GameObject Boss; // ������ ������

    public bool isWin = false;
    public bool isGameOver = false;

    private bool isESC = false;
    private bool isGameStart = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameStart)
        { 
            if(!isESC)
            {
                Time.timeScale = 0f; // ���� ����
                optionMenu.SetActive(true);
                isESC = true;
            }
            else
            {
                Time.timeScale = 1f; 
                optionMenu.SetActive(false);
                isESC = false;
            }
        }
        if (isGameOver)
        {
            deleteBullet();
            gameObj.SetActive(false);
            if(isWin)
            {
                gameOverWinMenu.SetActive(true);
            }
            else
            {
                gameOverDefeatMenu.SetActive(true);
            }
        }
    }

    public void main_gameStart()
    {
        isGameStart = true;
        gameObj.SetActive(true);
        mainMenu.SetActive(false);
        Instantiate(Boss);
    }

    public void option_returnToGame()
    {
        Time.timeScale = 1f; 
        optionMenu.SetActive(false);
        isESC = false;
    }

    public void gameover_restartGame()
    {
        gameObj.SetActive(true);
        isGameOver = false;
        Time.timeScale = 1f;
        if (isWin)
        {
            gameOverWinMenu.SetActive(false);
            Instantiate(Boss);
        }
        else
        {
            gameOverDefeatMenu.SetActive(false);
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Enemy");
            objectsWithTag[0].GetComponent<Boss>().respawn();
        }
        GameObject.Find("Player").GetComponent<Player>().respawn();
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

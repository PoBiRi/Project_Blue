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
    public GameObject EXBoss; // 생성할 프리팹

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
                Time.timeScale = 0f; // 게임 정지
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
        Instantiate(EXBoss);
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
            Instantiate(EXBoss);
        }
        else
        {
            gameOverDefeatMenu.SetActive(false);
        }
        GameObject.Find("Player").GetComponent<Player>().respawn();
    }

    void deleteBullet()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj); // 오브젝트 삭제
            Debug.Log(obj.name + " 삭제됨");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive; // 게임 정지 여부
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
    [Header("# GameObject")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    void Awake()
    {
        instance = this;
    }

    public void GameStart()
    {
        health = maxHealth;

        // 임시 스크립트 (첫번째 캐릭터 선택)
        uiLevelUp.Select(0);
        isLive = true;
        Resume();
    }

    public void GameOver() 
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine() 
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }

    // 게임 리트라이 -> 씬을 다시 로드
    public void GameRetry() 
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    // 경험치를 얻을 때
    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)]) {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    // 레벨 업 창이 오버레이 되었을 때 시간을 정지
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    // 레벨 업 창이 꺼졌을 때 정상 시간
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

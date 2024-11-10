using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스

    [Header("Game Control")]    // 게임 제어 관련 변수
    public bool isLive;         // 게임이 진행 중인지 확인
    public float gameTime;      // 게임 시간
    public float maxGameTime = 2 * 10f;  // 최대 게임 시간
    [Header("Player Info")]     // 플레이어 정보 관련 변수
    public int playerid;        // 플레이어 아이디
    public float health;        // 플레이어 체력
    public float maxHealth = 100;  // 최대 체력
    public int level;           // 플레이어 레벨
    public int kill;            // 처치 수
    public int exp;             // 경험치
    // 레벨업을 위한 경험치
    public int[] nextExp = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30 };

    [Header("Game Object")]     // 게임 객체 관련 변수
    public EnemyPool pool;      // 적 풀
    public PlayerController player; // 플레이어 컨트롤러
    public WindowController windowController; // 윈도우 컨트롤러
    public Result resultWindow; // 게임 결과 화면
    public GameObject enemyCleaner; // 적 청소 객체

    void Awake()
    {
        instance = this; // 싱글톤 인스턴스 설정
    }

    // 게임 시작
    public void GameStart(int id)
    {
        playerid = id;
        health = maxHealth;

        player.gameObject.SetActive(true);  // 플레이어 활성화
        windowController.Select(playerid % 2);  // 창 선택
        Resume();  // 게임 진행

        AudioManager.instance.PlayBgm(true); // 배경음악 시작
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select); // 선택 효과음
    }

    // 게임 승리
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    // 게임 승리 루틴
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);  // 적 청소 활성화

        yield return new WaitForSeconds(1.0f);  // 잠시 대기
    
        resultWindow.gameObject.SetActive(true);  // 결과 화면 활성화
        resultWindow.Victory();  // 승리 표시
        Stop();  // 게임 중지

        AudioManager.instance.PlayBgm(false); // 배경음악 정지
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win); // 승리 효과음
    }

    // 게임 오버
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    // 게임 오버 루틴
    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(3.3f);  // 잠시 대기

        resultWindow.gameObject.SetActive(true);  // 결과 화면 활성화
        resultWindow.Lose();  // 패배 표시
        Stop();  // 게임 중지

        AudioManager.instance.PlayBgm(false); // 배경음악 정지
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose); // 패배 효과음
    }

    // 게임 재시작
    public void GameRetry()
    {
        SceneManager.LoadScene(0);  // 첫 번째 씬을 로드하여 재시작
    }

    void Update()
    {
        if (!isLive)  // 게임이 진행 중이지 않으면 반환
            return;

        gameTime += Time.deltaTime;  // 게임 시간 갱신

        if (gameTime > maxGameTime)  // 최대 게임 시간 초과 시
        {
            gameTime = maxGameTime;  // 게임 시간을 최대값으로 설정
            GameVictory();  // 승리 처리
        }
    }

    // 경험치 획득
    public void GetExp()
    {
        if (!isLive)  // 게임이 진행 중이지 않으면 반환
            return;

        exp++;  // 경험치 증가

        // 경험치가 다음 레벨을 달성하면 레벨업
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;  // 레벨 증가
            exp = 0;   // 경험치 초기화
            windowController.Show();  // 창 표시
        }
    }

    // 게임 정지
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;  // 게임 일시 정지
    }

    // 게임 진행
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;  // 게임 재개
    }
}

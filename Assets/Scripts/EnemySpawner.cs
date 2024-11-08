using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 적 스폰 지점 배열과 스폰 데이터 배열
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    int level;

    // 타이머
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>(); // 자식 객체로부터 스폰 지점 가져오기
        // 게임 시간에 맞춰 레벨마다 스폰 간격 설정
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }

    // Update는 매 프레임마다 호출
    void Update()
    {
        if (!GameManager.instance.isLive) // 게임이 진행 중이 아니라면 반환
            return;

        timer += Time.deltaTime; // 타이머 증가
        // 레벨에 맞는 스폰 주기를 계산
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);

        if (timer > spawnData[level].spawnTime) // 스폰할 시간인지 확인
        {
            timer = 0; // 타이머 초기화
            Spawn(); // 적 스폰
        }   
    }

    // 적을 스폰하는 함수
    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(0);   // 풀에서 적 객체 가져오기
        
        // 랜덤 위치에서 적 스폰
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]); // 적 초기화
    }
}

// 스폰에 필요한 데이터 클래스
[System.Serializable]
public class SpawnData
{
    public float spawnTime; // 스폰 시간
    public int spriteType;  // 스프라이트 타입
    public int health;      // 적 체력
    public float speed;     // 적 이동 속도
}

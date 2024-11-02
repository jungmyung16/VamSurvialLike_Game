using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  업적(achievement)을 달성했을 때 캐릭터를 잠금 해제하고, UI에 알림을 표시하는 기능을 담당합니다.
    간단한 설명:
    AchiveManager: 게임의 업적을 관리하고, 캐릭터를 잠금 해제하며 UI 알림을 보여주는 클래스.
    lockCharacter, unlockCharacter: 잠금 상태인 캐릭터와 잠금 해제된 캐릭터를 각각 나타내는 배열.
    uiAchive: 업적 달성 시 표시되는 UI 객체.
    Achive enum: 달성할 수 있는 업적들을 나열한 열거형 (예: UnlockAgentC, UnlockAgentD).
    PlayerPrefs: 게임 데이터를 로컬 저장소에 저장하여, 게임을 재시작해도 데이터가 유지되게 함.
    주요 기능:
    Awake: PlayerPrefs에 저장된 데이터가 없으면 초기화.
    Init: 기본 데이터 초기화 (PlayerPrefs에 업적 상태 저장).
    Start: 게임 시작 시 잠금 상태의 캐릭터를 체크하여 표시.
    LateUpdate: 매 프레임마다 업적을 확인하고 달성 여부를 체크.
    CheckAchive: 특정 조건(예: 킬 수, 게임 시간)에 따라 업적을 확인하고 달성 시 UI 알림을 표시.
    AchiveRoutine: 업적 달성 시 UI를 잠시 표시하는 코루틴.
    핵심:
    업적 달성 시 캐릭터 잠금 해제
    UI 알림 표시 및 일정 시간 후 숨기기
    PlayerPrefs를 사용해 업적 상태 저장
 */

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiAchive;

    enum Achive { UnlockAgentC, UnlockAgentD }
    Achive[] achives;
    WaitForSecondsRealtime wait;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));

        wait = new WaitForSecondsRealtime(5);
        
        if(!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for(int i=0; i<lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(Achive chive in achives)
        {
            CheckAchive(chive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch(achive)
        {
            case Achive.UnlockAgentC:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockAgentD:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }
        if(isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
        
            for(int index=0; index<uiAchive.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiAchive.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(AchiveRoutine());
        }
    }

    IEnumerator AchiveRoutine()
    {
        uiAchive.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiAchive.SetActive(false);
    }
}

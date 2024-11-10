using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Hud : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health}  // 화면에 표시할 정보 종류

    Text myText;  // 텍스트 UI 컴포넌트
    Slider mySlider;  // 슬라이더 UI 컴포넌트

    void Awake()
    {
        myText = GetComponent<Text>();  // Text 컴포넌트 할당
        mySlider = GetComponent<Slider>();  // Slider 컴포넌트 할당
    }

    void LateUpdate()
    {
        switch (type)  // 표시할 정보 종류에 따라 처리
        {
            // 경험치 / 전체 경험치
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;  // 현재 경험치
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];  // 최대 경험치
                mySlider.value = curExp / maxExp;  // 슬라이더 값 갱신
                break;

            // 레벨
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);  // 레벨 텍스트 갱신
                break;

            // 처치 수
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instance.kill);  // 처치 수 텍스트 갱신
                break;

            // 시간
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;  // 남은 시간 계산
                int min = Mathf.FloorToInt(remainTime / 60);  // 남은 분
                int sec = Mathf.FloorToInt(remainTime % 60);  // 남은 초
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);  // 시간 텍스트 갱신
                break;

            // 체력
            case InfoType.Health:
                float curHealth = GameManager.instance.health;  // 현재 체력
                float maxHealth = GameManager.instance.maxHealth;  // 최대 체력
                mySlider.value = curHealth / maxHealth;  // 슬라이더 값 갱신
                break;
        }
    }
}

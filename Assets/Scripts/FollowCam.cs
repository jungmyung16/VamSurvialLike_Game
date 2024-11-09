using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    RectTransform rect;
    
    // Awake는 게임 오브젝트가 활성화될 때 호출, RectTransform 초기화
    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // FixedUpdate는 일정 시간 간격으로 호출, 카메라의 위치에 맞춰 UI 요소를 업데이트
    void FixedUpdate()
    {
        // 플레이어의 월드 좌표를 화면 좌표로 변환하여 UI 요소의 위치에 적용
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}

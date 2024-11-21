using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  아이템 창을 관리하는 스크립트, 아이템 선택 및 표시 등을 처리
 */

public class WindowController : MonoBehaviour
{
    RectTransform rect;  // 아이템 창의 RectTransform (크기 및 위치 조정)
    Item[] items;        // 창에 들어 있는 아이템들

    void Awake()
    {
        rect = GetComponent<RectTransform>();  // RectTransform 컴포넌트 가져오기
        items = GetComponentsInChildren<Item>(true);  // 아이템 컴포넌트를 자식들로부터 가져오기
    }
    
    // 창을 열 때 호출되는 메소드
    public void Show()
    {
        Next();  // 새로운 아이템을 랜덤으로 선택하여 표시
        rect.localScale = Vector3.one;  // 창 크기 복원 (보이도록 설정)
        GameManager.instance.Stop();  // 게임 일시 정지
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);  // 레벨업 효과음 재생
        AudioManager.instance.EffectBgm(true);  // 배경음악을 효과음으로 전환
    }

    // 창을 닫을 때 호출되는 메소드
    public void Hide()
    {
        rect.localScale = Vector3.zero;  // 창 크기를 0으로 설정 (숨김)
        GameManager.instance.Resume();  // 게임 재개
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);  // 선택 효과음 재생
        AudioManager.instance.EffectBgm(false);  // 배경음악 복원
    }

    // 주어진 index에 해당하는 아이템을 선택하고 클릭 이벤트 처리
    public void Select(int index)
    {
        items[index].OnClick();  // 해당 아이템 클릭 이벤트 실행
    }

    // 아이템을 랜덤으로 선택하여 다음 단계로 진행
    void Next()
    {
        // 1. 모든 아이템을 비활성화
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 2. 3개의 아이템을 랜덤하게 선택 (중복되지 않게)
        int[] ran = new int[3];
        while (true)
        {
            ran[0] = Random.Range(0, items.Length);  // 첫 번째 랜덤 아이템 선택
            ran[1] = Random.Range(0, items.Length);  // 두 번째 랜덤 아이템 선택
            ran[2] = Random.Range(0, items.Length);  // 세 번째 랜덤 아이템 선택

            // 중복되지 않도록 확인
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        // 3. 선택된 아이템을 활성화 (레벨에 맞게 처리)
        for (int index = 0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];  // 랜덤으로 선택된 아이템

            // 아이템의 레벨이 최대라면 해당 아이템을 제외하고 나머지 아이템을 활성화
            if (ranItem.level == ranItem.data.damages.Length)
            {
                items[4].gameObject.SetActive(true);  // 4번 인덱스 아이템을 활성화
            }
            else
            {
                ranItem.gameObject.SetActive(true);  // 해당 아이템을 활성화
            }
        }
    }
}

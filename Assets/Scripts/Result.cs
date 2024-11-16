using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // 결과 화면에서 표시할 제목 텍스트를 담고 있는 GameObject 배열
    public GameObject[] titles;

    // 게임이 패배했을 때 호출되는 함수
    public void Lose()
    {
        // 첫 번째 제목 (패배 텍스트)을 활성화
        titles[0].SetActive(true);
    }

    // 게임이 승리했을 때 호출되는 함수
    public void Victory()
    {
        // 두 번째 제목 (승리 텍스트)을 활성화
        titles[1].SetActive(true);
    }
}

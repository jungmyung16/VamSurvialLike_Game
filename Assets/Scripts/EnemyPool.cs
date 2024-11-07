using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    // 적 프리팹 배열
    public GameObject[] prefabs;

    // EnemyPool 객체 리스트 배열
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 프리팹의 길이에 맞춰 풀 배열 초기화

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>(); // 각 풀에 대해 빈 리스트 생성
        }

        Debug.Log(pools.Length); // 풀의 길이 출력 (디버깅용)
    }

    // 적 객체를 반환하는 함수
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 풀 내에서 비활성화된 적 객체를 찾는다.
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                // 비활성화된 객체를 찾아 select에 할당하고 활성화
                select = item;
                select.SetActive(true);
                break;
            }
        }
        
        // 비활성화된 객체가 없다면 새 객체를 생성
        if (!select)
        {
            // 새 객체 생성 후 풀에 추가
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select; // 선택된 적 객체 반환
    }
}

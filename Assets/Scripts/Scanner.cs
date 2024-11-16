using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // 탐지 범위
    public float scanRange;             
    // 탐지할 레이어 (타겟의 레이어)
    public LayerMask targetLayer;       
    // 탐지된 타겟들의 정보가 저장될 배열
    public RaycastHit2D[] targets;      
    // 가장 가까운 타겟의 Transform을 저장
    public Transform nearestTarget;     

    void FixedUpdate()
    {
        // 현재 위치를 중심으로 탐지 범위 내의 모든 타겟을 감지합니다.
        // CircleCastAll: 원형 범위 내에 있는 모든 타겟을 감지합니다.
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        // 가장 가까운 타겟을 찾음
        nearestTarget = GetNearest();
    }

    // 가장 가까운 타겟을 찾아 반환하는 함수
    Transform GetNearest()
    {
        Transform result = null;        // 결과값: 가장 가까운 타겟
        float diff = 100;               // 타겟과의 거리 차이를 추적할 변수, 초기값은 큰 값으로 설정

        // 탐지된 모든 타겟을 순회하며 가장 가까운 타겟을 찾음
        foreach(RaycastHit2D target in targets)
        {
            // 현재 위치
            Vector3 myPos = transform.position;             
            // 타겟의 위치
            Vector3 targetPos = target.transform.position;  
            // 두 지점 간의 거리 차이 계산
            float curDiff = Vector3.Distance(myPos, targetPos);

            // 더 가까운 타겟을 찾으면 그 타겟으로 갱신
            if(curDiff < diff)
            {
               diff = curDiff;
               result = target.transform;
            }
        }

        return result;  // 가장 가까운 타겟 반환
    }
}

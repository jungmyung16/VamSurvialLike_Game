using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHand : MonoBehaviour
{
    public bool isLeft; // 왼쪽 손인지 오른쪽 손인지 판단하는 변수
    public SpriteRenderer spriter;  // 손에 해당하는 스프라이트 렌더러

    SpriteRenderer player;  // 플레이어의 스프라이트 렌더러

    // 오른쪽 손 위치와 회전 값
    Vector3 rightPos = new Vector3(-0.915f, -0.592f, 0);
    Vector3 rightPosReverse = new Vector3(0.915f, -0.592f, 0);
    // 왼쪽 손 회전 값
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        // 부모 객체에서 2번째 스프라이트 렌더러를 가져옵니다. (보통은 플레이어 스프라이트 렌더러)
        player = GetComponentsInParent<SpriteRenderer>()[1];    
    }

    void LateUpdate()
    {
        // 플레이어가 좌우 반전되었는지 확인
        bool isReverse = player.flipX;

        if (isLeft) // 왼쪽 손일 경우
        {
            // 좌우 반전 여부에 따라 손의 회전을 설정합니다.
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;  // 좌우 반전되면 손도 반전됩니다.
            //spriter.sortingOrder = isReverse ? 4 : 4;  // sortingOrder는 정해진 값으로 설정 (필요시 주석 해제)
        }
        else // 오른쪽 손일 경우
        {
            // 오른쪽 손의 위치를 설정합니다. 반전된 상태에 따라 위치가 바뀝니다.
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;  // 좌우 반전되면 오른쪽 손도 반전됩니다.
            // 오른쪽 손의 sortingOrder는 좌우 반전 여부에 따라 다르게 설정됩니다.
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  게임 오브젝트가 플레이어의 위치에 따라 일정 조건에 맞는 위치로 이동하도록 하는 기능을 가진 스크립트입니다. 
 *  오브젝트가 "Area" 영역에서 벗어나면, 오브젝트의 종류에 따라 위치 이동이나 회전을 처리합니다.
 */

public class TilePosition : MonoBehaviour
{
    // Collider2D 타입의 콜라이더를 선언합니다.
    Collider2D col;

    // Awake 메소드: 게임 오브젝트의 콜라이더를 초기화합니다.
    void Awake()
    {
        col = GetComponent<Collider2D>();    
    }

    // OnTriggerExit2D 메소드: "Area"라는 태그를 가진 영역을 벗어날 때 발생하는 이벤트입니다.
    void OnTriggerExit2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 "Area" 태그를 가지고 있지 않으면 아무것도 하지 않습니다.
        if(!collision.CompareTag("Area"))
            return;
        
        // 플레이어의 현재 위치와 이 오브젝트의 위치를 비교하여 이동할 방향을 계산합니다.
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position; // 현재 오브젝트의 위치
        
        // 각 방향 (X, Y) 차이 계산
        switch(transform.tag)
        {
            // "Ground" 태그를 가진 오브젝트는 특정 조건에 따라 이동합니다.
            case "Ground":
                // 플레이어와 오브젝트의 X, Y 차이 계산
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                // X, Y 방향을 결정 (플레이어와의 상대적 방향)
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                // 차이를 절댓값으로 계산
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                // X와 Y의 차이를 비교하여 더 큰 값에 따라 이동
                if (diffX > diffY)
                {
                    // X축으로 이동
                    transform.Translate(Vector3.right * dirX * 40);
                }
                else if (diffX < diffY)
                {
                    // Y축으로 이동
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            // "Enemy" 태그를 가진 오브젝트는 일정 범위 내에서 랜덤으로 이동합니다.
            case "Enemy":
                // 콜라이더가 활성화된 상태일 때만 이동
                if (col.enabled)
                {
                    // 플레이어와 오브젝트의 차이를 계산하여 이동 방향을 결정
                    Vector3 dist = playerPos - myPos;
                    // 랜덤한 이동 범위를 계산
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);  

                    // 플레이어와의 거리와 랜덤한 벡터를 합쳐 이동
                    transform.Translate(ran + dist * 2);
                }
                break;
        }
    }
}

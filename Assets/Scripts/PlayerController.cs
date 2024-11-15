using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 입력, 이동 속도, 스캐너, 손과 애니메이션 컨트롤러 등을 다룹니다.
    public Vector2 inputVec; // 입력 벡터 (x: 가로, y: 세로)
    public float speed; // 플레이어의 이동 속도
    public Scanner scanner; // 스캐너 (추가 기능으로, 추후 코드에서 사용될 가능성 있음)
    public OnHand[] hands; // 왼손과 오른손을 나타내는 배열
    public RuntimeAnimatorController[] animCon; // 애니메이션 컨트롤러 배열 (플레이어 ID에 따라 다르게 설정)

    Rigidbody2D rigid; // Rigidbody2D (물리적 움직임을 제어하는 컴포넌트)
    SpriteRenderer spriteRen; // 스프라이트 렌더러 (플레이어 스프라이트를 관리)
    Animator anim; // 애니메이터 (플레이어 애니메이션 관리)

    // 게임 오브젝트가 활성화될 때 초기화
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // Rigidbody2D 가져오기
        spriteRen = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
        anim = GetComponent<Animator>(); // Animator 가져오기
        scanner = GetComponent<Scanner>(); // Scanner 가져오기 (추후 사용될 가능성)
        hands = GetComponentsInChildren<OnHand>(true); // 자식 객체로부터 OnHand 컴포넌트들을 가져오기
    }

    void OnEnable()
    {
        speed *= Character.Speed; // 플레이어의 이동 속도를 설정 (Character.Speed는 게임 내 설정값일 가능성 있음)
        anim.runtimeAnimatorController = animCon[GameManager.instance.playerid]; // 게임 매니저의 playerid에 따라 애니메이션 컨트롤러 설정
    }

    // 매 프레임마다 호출
    void Update()
    {
        if (!GameManager.instance.isLive) // 게임이 살아있지 않으면 동작을 멈춤
            return;

        // 플레이어의 입력 값 (가로, 세로)을 받아옴
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    // 물리적 업데이트 (실제 이동 처리는 FixedUpdate에서 수행)
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) // 게임이 살아있지 않으면 동작을 멈춤
            return;

        // 입력 값에 따라 이동 벡터를 계산하고 이동
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); // 이동
    }

    // 매 프레임 후 처리되는 함수
    void LateUpdate()
    {
        // 이동 속도에 따라 애니메이션 속도를 설정
        anim.SetFloat("Speed", inputVec.magnitude); 

        // 좌우 이동 시 스프라이트의 flipX 값을 조정하여 방향을 바꿈
        if(inputVec.x != 0)
        {
            spriteRen.flipX = inputVec.x > 0; // 오른쪽으로 가면 flipX를 false, 왼쪽으로 가면 true
        }
    }

    // 충돌 중일 때 호출되는 함수
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive) // 게임이 살아있지 않으면 동작을 멈춤
            return;

        // 지속적으로 체력이 감소
        GameManager.instance.health -= Time.deltaTime * 10;

        // 체력이 0보다 작아지면 게임 오버
        if(GameManager.instance.health < 0 )
        {
            // 자식 오브젝트(죽은 후 사라져야 할 객체들)를 비활성화
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            anim.SetTrigger("Dead"); // 'Dead' 애니메이션 트리거
            GameManager.instance.GameOver(); // 게임 오버 함수 호출
        }
    }
}

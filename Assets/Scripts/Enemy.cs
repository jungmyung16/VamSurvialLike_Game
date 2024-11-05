using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; // 적의 이동 속도
    public float health; // 적의 체력
    public float maxHealth; // 적의 최대 체력
    public RuntimeAnimatorController[] animController; // 애니메이터 컨트롤러 배열
    public Rigidbody2D target; // 타겟 (플레이어)

    bool isLive; // 적의 생사 여부

    Rigidbody2D rigid;
    Collider2D col;
    Animator anim;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>(); // Rigidbody2D 초기화
        col = GetComponent<Collider2D>(); // Collider2D 초기화
        anim = GetComponent<Animator>(); // Animator 초기화
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 초기화
        wait = new WaitForFixedUpdate();  // 고정 업데이트 대기
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) // 게임이 살아있지 않으면 이동하지 않음
            return;

        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))  // 적이 살아있지 않거나 'Hit' 애니메이션이면 이동하지 않음
            return;

        Vector2 dirVec = target.position - rigid.position; // 타겟 방향 계산
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 이동 방향과 속도 설정
        rigid.MovePosition(rigid.position + nextVec); // 이동
        rigid.velocity = Vector2.zero; // 속도 초기화
    }

    void LateUpdate()
    {
        if (!isLive) // 적이 죽었으면 업데이트 하지 않음
            return;

        spriteRenderer.flipX = target.position.x < rigid.position.x;  // 플레이어 위치에 따라 스프라이트 반전
    }

    // 객체가 활성화될 때 타겟과 상태 초기화
    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>(); // 타겟 설정 (플레이어)
        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriteRenderer.sortingOrder = 2;
        anim.SetBool("Dead", false); // 죽지 않은 상태로 설정
        health = maxHealth; // 체력 초기화
    }

    // 적 초기화
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animController[data.spriteType]; // 애니메이터 컨트롤러 설정
        speed = data.speed; // 속도 설정
        maxHealth = data.health; // 최대 체력 설정
        health = data.health; // 현재 체력 설정
    }

    // 총알과 충돌 시 처리
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive) // 총알과 충돌하거나 적이 죽었으면 무시
            return;

        health -= collision.GetComponent<Bullet>().damage; // 총알의 데미지만큼 체력 감소
        StartCoroutine(knockback()); // 넉백 처리

        if( health > 0 )
        {
            anim.SetTrigger("Hit"); // 'Hit' 애니메이션 실행
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit); // 적이 맞을 때 소리 재생
        }
        else
        {
            isLive = false; // 적이 죽음
            col.enabled = false; // 충돌 처리 안함
            rigid.simulated = false; // 물리 효과 비활성화
            spriteRenderer.sortingOrder = 1; // 스프라이트 순서 변경
            anim.SetBool("Dead", true); // 'Dead' 애니메이션 실행
            GameManager.instance.kill++; // 처치 수 증가
            GameManager.instance.GetExp(); // 경험치 획득

            if(GameManager.instance.isLive)
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead); // 죽을 때 소리 재생
        }
    }

    // 넉백 처리
    IEnumerator knockback()
    {
        yield return wait;  // 고정 업데이트 대기
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;    // 플레이어와의 방향 계산
        rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse); // 넉백 효과 적용
    }

    // 적이 죽었을 때 비활성화
    void Dead()
    {
        gameObject.SetActive(false); // 게임 오브젝트 비활성화
    }
}

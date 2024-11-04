using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;    // 총알의 피해량
    public int pen;         // 총알의 관통력

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    // Rigidbody2D 컴포넌트 초기화
    }

    public void Init(float damage, int pen, Vector3 dir) // 총알 초기화 (피해량, 관통력, 방향)
    {
        this.damage = damage;
        this.pen = pen;

        if(pen >= 0)
        {
            rigid.velocity = dir * 15f; // 관통력이 0 이상일 때 방향에 속도 적용
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 충돌하면 총알의 관통력을 차감하고, 관통력 0 이하이면 비활성화
        if (!collision.CompareTag("Enemy") || pen == -100)
            return;

        pen--;  // 관통력 차감

        if(pen < 0)
        {
            rigid.velocity = Vector2.zero; // 속도 0으로 설정
            gameObject.SetActive(false);   // 총알 비활성화
        }
    }

    // 총알이 'Area'를 벗어나면 비활성화
    void OnTriggerExit2D(Collider2D collision)
    {
        if(!collision.CompareTag("Area") || pen == -100)
            return ;

        gameObject.SetActive(false);   // 총알 비활성화
    }
}

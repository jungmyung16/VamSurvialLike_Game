using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;      // 무기 id
    public int prefabId;// 총알 프리팹의 id
    public float damage;// 무기 피해량
    public int count;   // 발사할 총알 수
    public float speed; // 회전 속도, 발사 속도

    float timer;       // 발사 타이머
    PlayerController player; // 플레이어 컨트롤러

    // Awake는 스크립트가 시작될 때 초기화 작업을 수행하는 메소드입니다.
    void Awake()
    {
        player = GameManager.instance.player;    
    }

    // Update는 매 프레임마다 호출됩니다. 여기서는 무기의 회전, 발사 등을 처리합니다.
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        // 무기 id에 따라 회전 혹은 발사 타이머를 처리
        switch (id)
        {
            case 0:
                // id가 0인 경우, 무기는 계속해서 회전
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                // 타이머가 발사 속도보다 커지면 발사
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

        // 테스트용 코드: 점프 버튼을 누르면 레벨업
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 5); // 레벨업 처리
        }
    }

    // 무기 레벨업 처리: 피해량과 총알 수 증가
    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage; // 캐릭터의 능력치를 반영한 피해량
        this.count += count; // 총알 수 증가

        if(id == 0)
            Batch(); // id가 0이면 회전 무기 처리

        // 무기 레벨업 후, Gear(장비)를 플레이어에 적용
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // 무기 초기화: 아이템 데이터에 따라 무기 설정
    public void Init(ItemData data)
    {
        // 기본 이름 설정
        name = "Weapon " + data.itemId;
        transform.parent = player.transform; // 플레이어의 자식으로 설정
        transform.localPosition = Vector3.zero; // 위치 초기화

        // 아이템 데이터에 따른 무기 설정
        id = data.itemId;
        damage = data.baseDamage * Character.Damage; // 기본 피해량에 캐릭터 능력치 반영
        count = data.baseCount + Character.Count; // 기본 총알 수에 캐릭터 능력치 반영
        
        // 프리팹 id 찾기
        for(int index = 0; index < GameManager.instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index; // 해당 프리팹 id 설정
                break;
            }
        }

        // 무기 id에 따른 설정
        switch (id)
        {
            case 0:
                // id가 0일 경우, 회전 속도 설정
                speed = 150 * Character.WeaponSpeed;
                Batch(); // 회전하는 무기 처리
                break;
            default:
                // 기본 발사 속도 설정
                speed = 0.5f * Character.WeaponRate;
                break;
        }

        // OnHand에 무기 손 위치 설정
        OnHand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand; // 손에 들어갈 무기 아이콘 설정
        hand.gameObject.SetActive(true); // 손에 무기 표시

        // 무기 장착 후, Gear(장비) 적용
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    // 회전하는 무기의 배치: 여러 총알을 일정 각도 간격으로 배치
    void Batch()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            // 자식 오브젝트가 있으면 그걸 사용, 없으면 풀에서 가져옴
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            // 총알의 초기 위치와 회전 설정
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // 총알을 회전시키는 벡터 생성 (등간격으로 회전)
            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); // 총알 발사

            // 총알 초기화
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -1은 총알의 방향 (반대 방향)
        }
    }

    // 타겟을 향해 총알을 발사하는 메소드
    void Fire()
    {
        // 타겟이 없으면 발사하지 않음
        if (!player.scanner.nearestTarget)
            return;

        // 타겟의 위치를 계산하여 발사 방향 설정
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        // 총알 생성 및 발사
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir); // 발사 방향 설정
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // 총알 초기화

        // 발사 효과음 재생
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}

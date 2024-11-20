using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGear : MonoBehaviour
{
    public ItemData.ItemType type;  // 장비 종류 (장갑, 신발 등)
    public float rate;              // 장비의 효과 수치 (데미지 증가율, 속도 증가율 등)

    // 장비 초기화: 아이템 데이터에 따른 설정
    public void Init(ItemData data)
    {
        // 기본 이름 설정
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instance.player.transform;  // 플레이어의 자식으로 설정
        transform.localPosition = Vector3.zero;  // 위치 초기화

        // 아이템 데이터에 따른 장비 타입과 효과 수치 설정
        type = data.itemType;
        rate = data.damages[0];  // 첫 번째 데미지 값을 rate로 설정 (기타 속성은 필요에 따라 사용)
        
        // 장비 효과 적용
        ApplyGear();
    }

    // 장비 레벨업: 효과 수치를 변경하고 적용
    public void LevelUp(float rate)
    {
        this.rate = rate;  // 새로운 효과 수치로 업데이트
        ApplyGear();  // 레벨업된 효과 적용
    }

    // 장비 효과 적용: 타입에 맞는 효과를 적용
    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Glove:
                // 장갑이라면 무기의 데미지 또는 속도에 영향을 줌
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                // 신발이라면 플레이어의 이동 속도에 영향을 줌
                SpeedUp();
                break;
        }
    }

    // 장갑에 의한 데미지 또는 속도 증가 적용
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();  // 플레이어의 모든 무기 가져오기

        // 각 무기에 대해 적용
        foreach (Weapon weapon in weapons)
        {
            switch(weapon.id)
            {
                case 0:
                    // 회전하는 무기 (id가 0인 무기)의 경우, 속도 증가
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = speed + (speed * rate);  // rate에 의한 속도 증가
                    break;
                default:
                    // 그 외 무기들의 경우, 발사 속도 감소
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = speed * (1f - rate);  // rate에 의한 발사 속도 감소
                    break;
            }
        }
    }

    // 신발에 의한 플레이어 이동 속도 증가 적용
    void SpeedUp()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;  // rate에 의한 속도 증가
    }
}

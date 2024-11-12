using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;  // 아이템의 데이터
    public int level;  // 아이템 레벨
    public Weapon weapon;  // 무기
    public WeaponGear gear;  // 장비 (장갑, 신발 등)

    // UI 컴포넌트들
    Image icon;  // 아이콘 이미지
    Text textLevel;  // 레벨 텍스트
    Text textName;  // 이름 텍스트
    Text textDesc;  // 설명 텍스트

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];  // 아이콘 이미지 할당
        icon.sprite = data.itemIcon;  // 아이템 아이콘 설정

        // 자식 텍스트 컴포넌트 할당
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;  // 아이템 이름 설정
    }

    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);  // 아이템 레벨 텍스트 설정

        // 아이템 종류에 따라 설명 텍스트 설정
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:  // 근접 무기
            case ItemData.ItemType.Range:  // 원거리 무기
                textDesc.text = string.Format(data.itemDes, data.damages[level] * 100, data.cnts[level]);
                break;
            case ItemData.ItemType.Glove:  // 장갑
            case ItemData.ItemType.Shoe:  // 신발
                textDesc.text = string.Format(data.itemDes, data.damages[level] * 100);
                break;
            default:
                textDesc.text = string.Format(data.itemDes);  // 기본 설명
                break;
        }
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            // 근접 무기 / 원거리 무기
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();  // 무기 초기화
                    weapon.Init(data);
                }
                else
                {
                    // 무기 레벨업
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.cnts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;  // 레벨 증가
                break;

            // 장갑 / 신발
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<WeaponGear>();  // 장비 초기화
                    gear.Init(data);
                }
                else
                {
                    // 장비 레벨업
                    float nextRate = data.damages[level];
                    gear.LevelUp(nextRate);
                }
                level++;  // 레벨 증가
                break;

            // 회복 아이템
            case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;  // 체력 회복
                break;
        }

        // 최대 레벨에 도달한 경우 버튼 비활성화
        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;  // 버튼 비활성화
        } 
    }
}

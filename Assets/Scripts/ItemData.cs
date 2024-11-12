using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject 
{
    // 아이템 타입을 정의하는 열거형 (근접 무기, 원거리 무기, 장갑, 신발, 회복 아이템)
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("Main Info")]
    public ItemType itemType;  // 아이템의 타입 (근접, 원거리, 장갑, 신발, 회복 등)
    public int itemId;         // 아이템 ID
    public string itemName;    // 아이템 이름

    [TextArea]  // 이 속성은 텍스트가 여러 줄일 수 있게 해줍니다.
    public string itemDes;  // 아이템의 설명
    public Sprite itemIcon; // 아이템의 아이콘 이미지

    [Header("Ability Data")]
    public float baseDamage;    // 기본 피해량
    public int baseCount;       // 기본 공격 횟수
    public float[] damages;     // 레벨업 시 증가하는 피해량
    public int[] cnts;          // 레벨업 시 증가하는 공격 횟수 (근접 무기: 공격 횟수 증가, 원거리 무기: 공격력 증가)

    [Header("Weapon")]
    public GameObject projectile;  // 원거리 무기의 발사체 (예: 총알, 화살 등)
    public Sprite hand;            // 근접 무기 장착 시 사용할 손 이미지
}

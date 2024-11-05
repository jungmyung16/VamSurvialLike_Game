using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed
    {
        get { return GameManager.instance.playerid == 0 ? 1.5f : 1f; } // 플레이어 ID가 0이면 속도 1.5, 아니면 1
    }

    public static float WeaponSpeed
    {
        get { return GameManager.instance.playerid == 1 ? 1.5f : 1f; } // 플레이어 ID가 1이면 무기 속도 1.5, 아니면 1
    }

    public static float WeaponRate
    {
        get { return GameManager.instance.playerid == 1 ? 0.9f : 1f; } // 플레이어 ID가 1이면 무기 공격 속도 0.9, 아니면 1
    }

    public static float Damage
    {
        get { return GameManager.instance.playerid == 2 ? 1.5f : 1f; } // 플레이어 ID가 2이면 데미지 1.5, 아니면 1
    }

    public static int Count
    {
        get { return GameManager.instance.playerid == 3 ? 1 : 0; } // 플레이어 ID가 3이면 1, 아니면 0
    }
}

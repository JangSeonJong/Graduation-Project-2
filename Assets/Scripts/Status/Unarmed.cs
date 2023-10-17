using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unarmed : WeaponBase
{
    protected override void Init()
    {
        weaponType = Define.WeaponType.Unarmed;
    }
}

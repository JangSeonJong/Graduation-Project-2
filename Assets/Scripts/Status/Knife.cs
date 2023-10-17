using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{
    protected override void Init()
    {
        weaponType = Define.WeaponType.Knife;
    }
}

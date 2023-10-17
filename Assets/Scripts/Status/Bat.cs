using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : WeaponBase
{
    protected override void Init()
    {
        weaponType = Define.WeaponType.Bat;
    }
}

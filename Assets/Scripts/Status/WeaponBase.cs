using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public Define.WeaponType weaponType  = Define.WeaponType.Unarmed;

    public GameObject weaponObject;

    void Start()
    {
        Init();
    }

    protected abstract void Init();

}

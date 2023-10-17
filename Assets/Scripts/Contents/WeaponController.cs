using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform currentWeapon;

    [SerializeField]
    WeaponBase[] weapons;

    [SerializeField]
    PlayerStatus status;


    void Start()
    {
        weapons = GetComponentsInChildren<WeaponBase>();
        status = GetComponentInParent<PlayerStatus>();
    }

    public void ChangeWeapon(Define.WeaponType weaponType, Item item)
    {
        if (currentWeapon != null)
        {
            WeaponBase equippedWeapon = currentWeapon.GetComponent<WeaponBase>();

            if (equippedWeapon != null)
            { 
                if (equippedWeapon.weaponType == weaponType)
                {
                    PutDownWeapon();
                }
                else
                {
                    EquipWeapon(weaponType, item);
                }

                equippedWeapon.weaponObject.SetActive(false);
            }
        }
        else
        {
            EquipWeapon(weaponType, item);
        }
    }

    public void PutDownWeapon()
    {
        currentWeapon = null;
        status.ResetStatus();
    }

    void EquipWeapon(Define.WeaponType weaponType, Item item)
    {
        weapons[(int)weaponType].weaponObject.SetActive(true);
        currentWeapon = weapons[(int)weaponType].GetComponent<Transform>();

        status.SetStatus(item.attack, item.attackDelay, item.attackRange);

    }
}

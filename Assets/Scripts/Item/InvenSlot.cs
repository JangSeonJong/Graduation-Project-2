using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlot : Slot
{
    WeaponController weaponController;
    PlayerStatus status;

    protected override void Init()
    {
        base.Init();

        weaponController = FindObjectOfType<WeaponController>();
        status = FindObjectOfType<PlayerStatus>();
    }

    protected override void SlotFunction()
    {
        UseItem(item);
    }

    public void UseItem(Item item)
    {
        if (item.itemType == Define.ItemType.Weapon)
        {
            EquippedWeapon(item);
        }

        else if (item.itemType == Define.ItemType.Used)
        {
            switch (item.itemPart)
            {
                case Define.ItemPart.Hunger:
                    {
                        status.SetHunger(item.effectValue);
                    }
                    break;

                case Define.ItemPart.Thirst:
                    {
                        status.SetThirst(item.effectValue);
                    }
                    break;
            }

            SetSlotCount(-1);
        }
    }

    public void EquippedWeapon(Item item)
    {
        isEquipped = !isEquipped;

        weaponController.ChangeWeapon(item.weaponType, item);

        // 장비 장착시 countImage On, 해제 시 countImage Off
        for (int i = 0; i < inven.invenSlots.Length; i++)
        {
            if (inven.invenSlots[i].isEquipped)
            {
                if (inven.equippedSlotIndex != i)
                {
                    if (inven.equippedSlotIndex != -1)
                    {
                        inven.invenSlots[inven.equippedSlotIndex].SetEquippedImage(false);
                        inven.invenSlots[inven.equippedSlotIndex].isEquipped = false;
                    }
                    inven.equippedSlotIndex = i;
                    inven.invenSlots[inven.equippedSlotIndex].SetEquippedImage(true);

                }
                else
                {
                    inven.invenSlots[inven.equippedSlotIndex].isEquipped = false;
                    inven.invenSlots[inven.equippedSlotIndex].SetEquippedImage(false);
                    inven.equippedSlotIndex = -1;
                }
            }
            else
            {
                if (inven.equippedSlotIndex == i)
                {
                    inven.invenSlots[inven.equippedSlotIndex].SetEquippedImage(false);
                    inven.equippedSlotIndex = -1;
                }
            }
        }
    }
}

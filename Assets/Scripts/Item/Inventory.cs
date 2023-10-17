using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryBase;

    [SerializeField]
    GameObject gridSet;

    bool inventoryActiavted = false;

    public Slot[] invenSlots;

    public int equippedSlotIndex = -1;

    void Start()
    {
        invenSlots = gridSet.GetComponentsInChildren<Slot>();
    }

    public void TryOpen()
    {
        inventoryActiavted = !inventoryActiavted;
        GameManager.isInvenOpen = inventoryActiavted;

        inventoryBase.SetActive(inventoryActiavted);
    }

    public void AcquireItem(Item item, int count = 1)
    {
        // 획득한 아이템이 장비 아이템인지, 장비면 빈 슬롯을 찾아 저장
        if (item.itemType != Define.ItemType.Weapon)
        {
            for (int i = 0; i < invenSlots.Length; i++)
            {
                // 슬롯 안에 아이템이 있다면
                if (invenSlots[i].item != null)
                {
                    // 슬롯 안의 아이템이 같은 아이템이라면
                    if (invenSlots[i].item.itemName == item.itemName)
                    {
                        // 그 슬롯의 count 를 1 증가
                        invenSlots[i].SetSlotCount(count);

                        return;
                    }
                }
            }
        }

        // 슬롯에 같은 아이템이 없어서 빠져 나왔다면 다시 for문 실행
        for (int i = 0; i < invenSlots.Length; i++)
        {
            // 슬롯 안에 아이템이 없다면
            if (invenSlots[i].item == null)
            {
                // 슬롯에 아이템 정보 저장
                invenSlots[i].AddItem(item, count);

                return;
            }
        }
    }
}

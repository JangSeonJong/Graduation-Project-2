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
        // ȹ���� �������� ��� ����������, ���� �� ������ ã�� ����
        if (item.itemType != Define.ItemType.Weapon)
        {
            for (int i = 0; i < invenSlots.Length; i++)
            {
                // ���� �ȿ� �������� �ִٸ�
                if (invenSlots[i].item != null)
                {
                    // ���� ���� �������� ���� �������̶��
                    if (invenSlots[i].item.itemName == item.itemName)
                    {
                        // �� ������ count �� 1 ����
                        invenSlots[i].SetSlotCount(count);

                        return;
                    }
                }
            }
        }

        // ���Կ� ���� �������� ��� ���� ���Դٸ� �ٽ� for�� ����
        for (int i = 0; i < invenSlots.Length; i++)
        {
            // ���� �ȿ� �������� ���ٸ�
            if (invenSlots[i].item == null)
            {
                // ���Կ� ������ ���� ����
                invenSlots[i].AddItem(item, count);

                return;
            }
        }
    }
}

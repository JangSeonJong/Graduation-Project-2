using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // �� Spawner���� ������ �����۰� ���� ����
    public Dictionary<Item, int> Items { get; private set; } = new Dictionary<Item, int>();
    // ������ų item ���� ����
    [SerializeField]
    List<Item> itemDB = new List<Item>();

    // Dictionary�� ������ ������ ���� �ӽ� ����
    Item spawnItem;
    // Dictionary�� ������ ������ ���� �ӽ� ����
    int spawnCount;

    // ������ų ������ ���� UI�� �θ�
    public GameObject spawnSlotSet;

    // ���� ������ ������ ���� UI �迭
    public Slot[] spawnSlots;

    public void SpawnItem()
    {
        // ������ų ������ ������ŭ �ݺ�
        for (int index = 0; index < itemDB.Count; index++)
        {
            // ������ų �����۸��� �ٸ��� ����
            spawnItem = itemDB[index];
            spawnCount = RandomSpawnCount(spawnItem);

            // �� ������ ������ ������ Dictionary�� ����
            if (Items.ContainsKey(spawnItem))
                Items[spawnItem] = spawnCount;
            else
                Items.Add(spawnItem, spawnCount);
        }
    }

    // �� �������� ���� Ȯ���� ���� ������ų ������ ���� ���
    int RandomSpawnCount(Item item)
    {
        int count = 0;

        for (int i = 0; i < item.maxChanceCount; i++)
        {
            int random = Random.Range(1, 101);

            if (random <= item.spawnPercentage)
                count++;
        }

        return count;
    }

    // �÷��̾ Spawner�� ��ȣ�ۿ��ϸ� �������� UI�� ���� �־���
    public void AddSpawnSlot()
    {
        // ������ ������ ������ŭ �ݺ�
        for (int index = 0; index < Items.Count; index++)
        {
            // �ش� �������� ������ 0�� �ƴϸ� ����
            if (Items[itemDB[index]] != 0)
            {
                spawnItem = itemDB[index];
                spawnCount = Items[itemDB[index]];

                // ���������� ������ŭ �ݺ�
                for (int i = 0; i < spawnSlots.Length; i++)
                {
                    // �������� ������� ���� ������ ã�� ����
                    if (spawnSlots[i].item == null)
                    {
                        spawnSlots[i].AddItem(spawnItem, spawnCount);

                        break;
                    }
                }
            }
        }
    }

    // �÷��̾ Spawner���� ������ ����
    public void ClearSpawnSlot()
    {
        // ���������� ������ŭ �ݺ�
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            // �������� ����� ���������� ã�� Clear��Ŵ
            if (spawnSlots[i].item != null)
            {
                spawnSlots[i].ClearSlot();
            }
        }
    }

    // ���� ���Կ��� �������� �������� Dictionarty���� ���Ž�����
    public void ClearSpawnItem(Item item)
    {
        if (item != null)
        {
            if (Items.ContainsKey(item))
            {
                Items[item] = 0;
            }
        }
    }

    public void ClearSpawner()
    {
        Items.Clear();
    }
}

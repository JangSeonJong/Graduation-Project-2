using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // 각 Spawner에서 스폰될 아이템과 개수 저장
    public Dictionary<Item, int> Items { get; private set; } = new Dictionary<Item, int>();
    // 스폰시킬 item 정보 저장
    [SerializeField]
    List<Item> itemDB = new List<Item>();

    // Dictionary에 저장할 아이템 정보 임시 변수
    Item spawnItem;
    // Dictionary에 저장할 아이템 개수 임시 변수
    int spawnCount;

    // 스폰시킬 아이템 슬롯 UI의 부모
    public GameObject spawnSlotSet;

    // 스폰 아이템 보여줄 슬롯 UI 배열
    public Slot[] spawnSlots;

    public void SpawnItem()
    {
        // 스폰시킬 아이템 종류만큼 반복
        for (int index = 0; index < itemDB.Count; index++)
        {
            // 스폰시킬 아이템마다 다르게 설정
            spawnItem = itemDB[index];
            spawnCount = RandomSpawnCount(spawnItem);

            // 각 아이템 정보와 개수를 Dictionary에 저장
            if (Items.ContainsKey(spawnItem))
                Items[spawnItem] = spawnCount;
            else
                Items.Add(spawnItem, spawnCount);
        }
    }

    // 각 아이템의 생성 확률에 따라 생성시킬 아이템 개수 계산
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

    // 플레이어가 Spawner에 상호작용하면 스폰슬롯 UI에 정보 넣어줌
    public void AddSpawnSlot()
    {
        // 스폰된 아이템 개수만큼 반복
        for (int index = 0; index < Items.Count; index++)
        {
            // 해당 아이템의 개수가 0이 아니면 실행
            if (Items[itemDB[index]] != 0)
            {
                spawnItem = itemDB[index];
                spawnCount = Items[itemDB[index]];

                // 스폰슬롯의 개수만큼 반복
                for (int i = 0; i < spawnSlots.Length; i++)
                {
                    // 아이템이 저장되지 않은 슬롯을 찾아 저장
                    if (spawnSlots[i].item == null)
                    {
                        spawnSlots[i].AddItem(spawnItem, spawnCount);

                        break;
                    }
                }
            }
        }
    }

    // 플레이어가 Spawner에서 나갈때 실행
    public void ClearSpawnSlot()
    {
        // 스폰슬롯의 개수만큼 반복
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            // 아이템이 저장된 스폰슬롯을 찾아 Clear시킴
            if (spawnSlots[i].item != null)
            {
                spawnSlots[i].ClearSlot();
            }
        }
    }

    // 스폰 슬롯에서 아이템을 가져가면 Dictionarty에서 제거시켜줌
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

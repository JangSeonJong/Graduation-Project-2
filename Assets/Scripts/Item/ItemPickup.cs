using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    bool spawnTableOpen = false; // spawner 관련

    // spawner를 열때 보여줄 UI
    [SerializeField]
    GameObject spawnTable; // spawner 관련

    public ItemSpawner spawner; // spawner 관련

    public void CheckItem(RaycastHit hit, bool isSearch)
    {
        if (isSearch) // spawner 관련
        {
            // 접근한 spawner 정보 저장
            spawner = hit.transform.GetComponent<ItemSpawner>();
            if (spawner.Items.Count <= 0)
                spawner.SpawnItem();
            Searchitem();
        }
        else
        {
            spawnTableOpen = false; // spawner 관련
            CloseSpawnTable(); // spawner 관련
        }
    }

    // SpawnSlot을 On/Off 시키는 함수
    void Searchitem()
    {
        spawnTableOpen = !spawnTableOpen;
        GameManager.isSpawnerOpen = spawnTableOpen;

        if (spawnTableOpen)
            OpenSpawnTable();
        else
            CloseSpawnTable();
    }

    void OpenSpawnTable()
    {
        // Spawner를 열때 해당 Spawner에 저장된 정보를 SpawnSlot에 저장
        if (spawner != null)
        {
            spawnTable.SetActive(true);
            spawner.spawnSlotSet = GameObject.FindGameObjectWithTag("SpawnSlotSet");
            spawner.spawnSlots = spawner.spawnSlotSet.GetComponentsInChildren<Slot>();
            spawner.AddSpawnSlot();
        }
    }

    void CloseSpawnTable()
    {
        // Spawner를 닫을때 해당 Spawner에 저장된 정보를 SpawnSlot에서 삭제
        if (spawner != null)
            spawner.ClearSpawnSlot();

        spawner = null;
        spawnTable.SetActive(false);

        if (DragSlot.instance != null)
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }
}

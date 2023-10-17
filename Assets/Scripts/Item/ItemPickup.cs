using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    bool spawnTableOpen = false; // spawner ����

    // spawner�� ���� ������ UI
    [SerializeField]
    GameObject spawnTable; // spawner ����

    public ItemSpawner spawner; // spawner ����

    public void CheckItem(RaycastHit hit, bool isSearch)
    {
        if (isSearch) // spawner ����
        {
            // ������ spawner ���� ����
            spawner = hit.transform.GetComponent<ItemSpawner>();
            if (spawner.Items.Count <= 0)
                spawner.SpawnItem();
            Searchitem();
        }
        else
        {
            spawnTableOpen = false; // spawner ����
            CloseSpawnTable(); // spawner ����
        }
    }

    // SpawnSlot�� On/Off ��Ű�� �Լ�
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
        // Spawner�� ���� �ش� Spawner�� ����� ������ SpawnSlot�� ����
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
        // Spawner�� ������ �ش� Spawner�� ����� ������ SpawnSlot���� ����
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

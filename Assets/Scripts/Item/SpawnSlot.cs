using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnSlot : Slot
{
    protected override void SlotFunction()
    {
        inven.AcquireItem(item, itemCount);
        itemPickup.spawner.ClearSpawnItem(item);
        ClearSlot();
    }

    protected override void ChangeSlot()
    {
        base.ChangeSlot();

        if (itemPickup.spawner.Items.ContainsKey(DragSlot.instance.dragSlot.item))
            itemPickup.spawner.Items[DragSlot.instance.dragSlot.item] += DragSlot.instance.dragSlot.itemCount;
        else
            itemPickup.spawner.Items.Add(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
    }
}

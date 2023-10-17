using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public abstract class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImage;

    [SerializeField]
    protected Text countText;
    [SerializeField]
    protected GameObject countImage;

    protected Inventory inven;
    protected ItemPickup itemPickup;

    public bool isEquipped = false;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        inven = FindObjectOfType<Inventory>();
        itemPickup = FindObjectOfType<ItemPickup>();
    }

    void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    public void AddItem(Item item, int count = 1)
    {
        if (item == null)
        {
            return;
        }
         
        this.item = item;
        itemCount = count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Define.ItemType.Weapon)
        {
            countImage.SetActive(true);
            countText.text = itemCount.ToString();
        }
        else
        {
            countText.text = "";
            countImage.SetActive(false);
        }

        SetColor(1);
    }

    public void SetSlotCount(int count)
    {
        itemCount += count;
        countText.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        countText.text = "";
        countImage.SetActive(false);
    }

    protected abstract void SlotFunction();

    public void OnPointerClick(PointerEventData eventData)
    {
        // 스크립트가 적용된 객체에 우클릭을 하면 실행
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                SlotFunction();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            if (DragSlot.instance.dragSlot.item != item)
            {
                if (itemPickup.spawner != null)
                    itemPickup.spawner.ClearSpawnItem(DragSlot.instance.dragSlot.item);
                ChangeSlot();
            }
            else
            {
                if (DragSlot.instance.dragSlot != this)
                {
                    itemPickup.spawner.ClearSpawnItem(DragSlot.instance.dragSlot.item);
                    SetSlotCount(DragSlot.instance.dragSlot.itemCount);
                    DragSlot.instance.dragSlot.ClearSlot();
                }
            }
        }
    }

    protected virtual void ChangeSlot()
    {
        Item tmpItem = item;
        int tmpCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (tmpItem != null)
            DragSlot.instance.dragSlot.AddItem(tmpItem, tmpCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    public void SetEquippedImage(bool active)
    {
        if (item == null)
            return;

        if (item.itemType == Define.ItemType.Weapon)
        {
            countImage.SetActive(active);
        }

    }
}

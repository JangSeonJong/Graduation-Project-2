using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    public static DragSlot instance;

    public Slot dragSlot;

    [SerializeField]
    Image dragImage;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image itemImage)
    {
        dragImage.sprite = itemImage.sprite;
        SetColor(1);
    }

    public void SetColor(float alpha)
    {
        Color color = dragImage.color;
        color.a = alpha;
        dragImage.color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;

    public Define.ItemType itemType;
    public Define.WeaponType weaponType;
    public Define.ItemPart itemPart;

    public int spawnPercentage;
    public int maxChanceCount;

    public int effectValue;
    public int attack;
    public float attackDelay;
    public float attackRange;
}

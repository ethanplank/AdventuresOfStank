using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
   public enum ItemType
    {
        LaserSword,
        LaserGun,
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch(itemType)
        {
            default:
            case ItemType.LaserSword: return ItemAssets.Instance.swordSprite;
            case ItemType.LaserGun: return ItemAssets.Instance.gunSprite;
        }
    }
}

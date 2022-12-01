using UnityEngine;

public class Item
{
    //Gives distinct types of what Items that can be held in inventory   
    public enum ItemType
    {
        LaserSword,
        LaserGun,
    }
    public ItemType itemType;
    public int amount;

    //Returns the sprite that is needed to be displayed from ItemAssets
    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.LaserSword: return ItemAssets.Instance.swordSprite;
            case ItemType.LaserGun: return ItemAssets.Instance.gunSprite;
        }
    }
}

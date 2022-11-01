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
}

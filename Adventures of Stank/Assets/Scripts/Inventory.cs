using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
        
        
       // Debug.Log(itemList.Count);
    }
    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
    public void addSword()
    {
        AddItem(new Item { itemType = Item.ItemType.LaserSword, amount = 1 });
    }
    public void addGun()
    {
        AddItem(new Item { itemType = Item.ItemType.LaserGun, amount = 1 });
    }
}

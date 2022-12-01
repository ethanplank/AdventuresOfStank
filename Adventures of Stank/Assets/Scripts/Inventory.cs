using System.Collections.Generic;

public class Inventory
{
    private List<Item> itemList;
    //Creates a new list that contains the items in Stank's inventory
    public Inventory()
    {
        itemList = new List<Item>();

    }
    //Return method for itemList
    public List<Item> GetItemList()
    {
        return itemList;
    }
    //Tells AddItem to add Stank's Laser Sword
    public void addSword()
    {
        AddItem(new Item { itemType = Item.ItemType.LaserSword, amount = 1 });
    }
    //Tells AddItem to add Stank's Laser Gun
    public void addGun()
    {
        AddItem(new Item { itemType = Item.ItemType.LaserGun, amount = 1 });
    }
    //Adds an item to Stank's inventory
    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}

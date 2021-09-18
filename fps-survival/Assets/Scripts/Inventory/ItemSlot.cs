using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    public bool isEmpty => itemAmount == 0;

    Item item;
    int itemAmount;
    
    Charecter owner;
    Inventory ownerInventory;

    public void AddItem(Item _item, int amount)
    {

        item = _item;
        itemAmount += amount;
    }

    public void RemoveItem(int amount)
    {
        itemAmount -= amount;
        if (itemAmount <= 0)
        {
            item = null;
            itemAmount = 0;
        }
    }

    public bool CanAcceptItem(Item _item)
    {
        if (item == _item && itemAmount < item.GetStackSize(owner._storageType) || isEmpty) { return true; } else return false;
    }

    public bool isSameItem(Item _item)
    {
        return item == _item;
    }

    public int GetItemAmount()
    {
        return itemAmount;
    }

    public Item GetItem()
    {
        return item;
    }

    public void SetOwnerAndSlot(Charecter _owner)
    {
        owner = _owner;
        ownerInventory= owner.GetComponent<Inventory>();
    }

    public void UseItem()
    {
        if(item is IUsable)
        {
            (item as IUsable).Use(ownerInventory,owner);
        }
    }
}

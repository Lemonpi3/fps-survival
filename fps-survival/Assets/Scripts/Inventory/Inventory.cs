using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<int,ItemSlot> inventory = new Dictionary<int,ItemSlot>();

    [SerializeField] int inventorySize = 40;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] GameObject slotUIprefab;
    [SerializeField] ItemSlotParentUI slotParentUI;

    private Charecter owner;

    public bool isMenuOpen { get { if (inventoryUI != null) { return inventoryUI.gameObject.activeSelf; } else return false; } }

    [Header("Testing porpuse")]
    public Item testItem;
    public int testAmount;
    public int removeAmount;


    private void Start()
    {
        owner = GetComponent<Charecter>();
        AddSlots(inventorySize*2);
        AddItem(testItem, testAmount);
        RemoveItem(testItem, removeAmount);
    }

    public void AddSlots(int amount=0)
    {
        if(amount- inventorySize <= 0 || inventoryUI == null) { return; }

        for (int i = 0; i < amount-inventorySize; i++)
        {
            ItemSlotUI slot = (Instantiate(slotUIprefab, slotParentUI.transform).GetComponent<ItemSlotUI>());
            slot.gameObject.name = "Slot " + i;
            slotParentUI.slots.Add(slot);
            inventory.Add(i, new ItemSlot());
            slot.SetItemSlot(inventory[i],owner);
        }
    }

    public bool AddItem(Item item,int amount)
    {
        int amountRemaining = amount;

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].CanAcceptItem(item) && amountRemaining > 0)
            {
                int amountThatCanBeAdded =item.GetStackSize(owner._storageType)- inventory[i].GetItemAmount();

                if(amountRemaining >= amountThatCanBeAdded)
                {
                    inventory[i].AddItem(item, amountThatCanBeAdded);
                    amountRemaining -= amountThatCanBeAdded;
                    slotParentUI.RefreshSlot(i);
                    Debug.Log("Added " + amountThatCanBeAdded + " of " + item);
                }
                else
                {
                    inventory[i].AddItem(item, amountRemaining);
                    slotParentUI.RefreshSlot(i);

                    Debug.Log("Added " + amount + " of " + item);
                    return true;                                                //succsesfully added all items
                }
            }
        }
        if (amountRemaining != amount || amountRemaining != 0)
        {
            Debug.Log("Couldn't Add all items added " +(amount-amountRemaining) +" of "+ item);
            return true; 
        }
        else
        {
            Debug.Log("Couldn't Add " + item);
            return false;
        }
        
    }

    public void RemoveItem(Item item,int amount)
    {
        int amountRemaining = amount;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].isSameItem(item) && amountRemaining > 0)
            {
                if (amountRemaining > inventory[i].GetItemAmount())
                {
                    Debug.Log("Removed " + inventory[i].GetItemAmount() + " of " + item + " in " + i);
                    amountRemaining -= inventory[i].GetItemAmount();
                    inventory[i].RemoveItem(inventory[i].GetItemAmount());
                    slotParentUI.RefreshSlot(i);
                }
                else
                {
                    inventory[i].RemoveItem(amountRemaining);
                    amountRemaining = 0;
                    slotParentUI.RefreshSlot(i);
                }
            }
            else if(amountRemaining > 0)
            {
                continue;
            }
            else { Debug.Log(inventory[i].GetItem() + " " + inventory[i].GetItemAmount()); break; }
        }
        Debug.Log("Removed " + (amount - amountRemaining) + " of " + amount);
    }

    public int CheckAmountInInventory(Item item)
    {
        int amount=0;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventory[i].isSameItem(item))
            {
                amount += inventory[i].GetItemAmount();
            }
        }
        return amount;
    }

    /// <summary>
    /// Returns a dictionary with the amount of eachItem in the array
    /// </summary>
    public Dictionary<Item,int> GetAllItemsAmountFromAnArray(Item[] items)
    {
        Dictionary<Item, int> itemsToReturn = new Dictionary<Item, int>();
        
        for (int i = 0; i < items.Length; i++)
        {
            itemsToReturn.Add(items[i], CheckAmountInInventory(items[i]));
        }

        return itemsToReturn;
    }

    public void ToggleUI()
    {
        if(inventoryUI != null)
            inventoryUI.gameObject.SetActive(!isMenuOpen);
    }
}

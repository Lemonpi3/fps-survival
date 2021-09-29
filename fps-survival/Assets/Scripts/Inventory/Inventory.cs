using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<int,ItemSlot> inventory = new Dictionary<int,ItemSlot>();

    [SerializeField] int maxInventorySize = 40; //maximum amount the charecter can have
    [SerializeField] int currentInventorySize = 40; //amount of storage aviable to use
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] GameObject slotUIprefab;
    [SerializeField] ItemSlotParentUI slotParentUI;

    private Charecter owner;

    [Header("Testing porpuse")]
    public Item testItem;
    public int testAmount;
    public int removeAmount;


    private void Start()
    {
        LoadInventoryUI();
        ShowSomeSlots(currentInventorySize);
    }

    //<<<<<<<<<<<----------Inventory Main Functions------->>>>>>>>>>
    #region Inventory Functions
    public void CreateSlots(int amount=0)
    {
        if(maxInventorySize <= 0 || inventoryUI == null) { return; }

        for (int i = 0; i < amount; i++)
        {
            ItemSlotUI slot = (Instantiate(slotUIprefab, slotParentUI.transform).GetComponent<ItemSlotUI>());
            slot.gameObject.name = "Slot " + i;
            slotParentUI.slots.Add(slot);
            inventory.Add(i, new ItemSlot());
            slot.SetItemSlot(inventory[i],owner,this);
        }
    }

    public bool AddItem(Item item,int amount)
    {
        int amountRemaining = amount;

        for (int i = 0; i < maxInventorySize; i++)
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
        if(item == null) { return; }

        int amountRemaining = amount;
        for (int i = 0; i < maxInventorySize; i++)
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

    /// <summary>
    /// Returns the amount that is in the inventory
    /// </summary>
    public int CheckAmountInInventory(Item item)
    {
        int amount=0;
        for (int i = 0; i < maxInventorySize; i++)
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

    /// <summary>
    /// Trys to get the amount of the item given
    /// </summary>
    /// <returns>Dictionary of the Item and the amount or Null if the item is not found in the inventory or the amount aviable is less than requested</returns>
    public Dictionary<Item, int> GetItemFromTheInventory(Item item,int amount)
    {
        Dictionary<Item, int> itemsToReturn = new Dictionary<Item, int>();
        int amountToAdd = CheckAmountInInventory(item);

        if(amountToAdd <= 0) { Debug.Log("Item Not found"); return null; }

        if(amountToAdd < amount) { Debug.Log("Item Not found"); return null; }
        
        else
        {
            itemsToReturn.Add(item, amount);
            RemoveItem(item, amount);
        }
        return itemsToReturn;
    }

    /// <summary>
    /// Gets item from other inventory,fails if the item is not found in the inventory or the amount aviable is less than requested
    /// </summary>
    public void TryToGetItemFromOtherInventory(Item item, int amount,Inventory targetInventory)
    {
        Dictionary<Item, int> itemToAdd = targetInventory.GetItemFromTheInventory(item, amount);
        if (itemToAdd.ContainsKey(item) != item || itemToAdd == null) { Debug.Log("Failed to get the item: " + item+" amount Requested: "+ amount); return; }

        AddItem(item, itemToAdd[item]);
    }
    #endregion

    //<<<<<<<<<<<<<----------Utils Functions----------->>>>>>>>>>
    #region Inventory Utils Functions
    /// <summary>
    /// Sets the user of the inventoryUI, and can set a new ownership from an existing inventory
    /// </summary>
    public void ChangeNewUser(Charecter newOwner,Inventory inventory)
    {
        slotParentUI.ChangeOwner(newOwner,inventory);
        owner = newOwner;
    }

    public Charecter GetOwner()
    {
        return owner;
    }

    public GameObject GetInventoryUI()
    {
        return inventoryUI.gameObject;
    }
    #endregion

    //<<<<<<<<<<<<<<<<<<<-------------INVENTORY UI Functions----------------->>>>>>>>>>>>>>
    #region InventoryUI Functions
    public void LoadInventoryUI()
    {
        owner = GetComponent<Charecter>();
        if (slotParentUI.slots.Count != maxInventorySize && inventoryUI != null)
        {
            CreateSlots(maxInventorySize);
        }
    }

    public void ChangeInventoryUIPos(Vector3 newPos)
    {
        inventoryUI.ChangeInventoryPos(newPos);
    }

    public void ResetInventoryUIPos()
    {
        inventoryUI.ResetPos();
    }

    public void ShowSomeSlots(int amount)
    {
        slotParentUI.ShowASetAmountOfSlots(amount);
    }
    #endregion
}

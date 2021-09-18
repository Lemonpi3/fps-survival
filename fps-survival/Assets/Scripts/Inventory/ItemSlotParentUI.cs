using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotParentUI : MonoBehaviour
{
    public List<ItemSlotUI> slots = new List<ItemSlotUI>();

    public void RefreshAllSlots()
    {
        foreach(ItemSlotUI slot in slots)
        {
            slot.RefreshSlotUI();
        }
    }

    public void RefreshSlot(int idx)
    {
        slots[idx].RefreshSlotUI();
    }

    public void DisableAllSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    public void ShowASetAmountOfSlots(int amount)
    {
        DisableAllSlots();
        if(amount > slots.Count ) { amount = slots.Count; }

        for (int i = 0; i < amount; i++)
        {
            slots[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Changes the owner of the inventory UI to a new one
    /// </summary>
    public void ChangeOwner(Charecter newOwner,Inventory Inventory)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].SetItemSlot(slots[i].GetSlot(), newOwner, Inventory);
        }
    }
}

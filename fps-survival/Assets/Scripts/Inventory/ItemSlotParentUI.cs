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
}

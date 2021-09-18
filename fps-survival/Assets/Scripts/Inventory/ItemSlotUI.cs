using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text stackCount;
    [SerializeField] Sprite emptySlotSprite;
    
    int itemAmount;
    private Item item;
    private ItemSlot slot;

    public bool isEmpty => itemAmount == 0;

    public void RefreshSlotUI()
    {
        if (slot.isEmpty)
        {
            icon.sprite = emptySlotSprite;
            stackCount.text = null;
            item = null;
            itemAmount = 0;
        }
        else
        {
            item = slot.GetItem();
            itemAmount = slot.GetItemAmount();
            icon.sprite = item.icon;
            stackCount.text = itemAmount.ToString();
        }
    }

    public void SetItemSlot(ItemSlot _slot,Charecter owner,Inventory inventory)
    {
        slot = _slot;
        slot.SetOwnerAndSlot(owner,inventory);
    }

    public int GetItemAmount()
    {
        return itemAmount;
    }

    public void UseItem()
    {
        slot.UseItem();
    }

    public ItemSlot GetSlot()
    {
        return slot;
    }
}

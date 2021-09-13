using TMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Item item;
    private Image icon;

    [SerializeField]
    private TMP_Text itemName;
    [SerializeField]
    private TMP_Text itemDescription;

    public int itemAmount;

    public void AddItem(Item _item , int _itemAmount = 1)
    {
        item = _item;
        icon.sprite = item.icon;
        itemName.text = item.itemName;
        itemDescription.text = item.description;
        itemAmount += _itemAmount;
    }

    public void UseItem()
    {
        item.OnUseItem();
    }

    public void RemoveItem(int amount)
    {
        itemAmount -= amount;
        if(itemAmount <= 0)
        {
            item = null;
            icon.sprite = null;
            itemName.text = null;
            itemDescription.text = null;
            itemAmount = 0;
        }
    }
}

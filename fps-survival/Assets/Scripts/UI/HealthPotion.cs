using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion",menuName =("Items/Consumables/New HealthPotion"))]
public class HealthPotion : Item,IUsable
{
    [SerializeField] int _healAmount=1;

    [SerializeField] bool consumesItem;
    [SerializeField] int amountOfItemsThatConsumes=1;

    public void Use(Inventory inventory = null, Charecter owner = null, Charecter target = null)
    {
        if (inventory.CheckAmountInInventory(this) < amountOfItemsThatConsumes) { Debug.Log("Not enought items"); return; }

        owner.Heal(_healAmount);
        if (consumesItem)
        {
            inventory.RemoveItem(this, amountOfItemsThatConsumes);
        }
    }
}

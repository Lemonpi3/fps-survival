using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Interactable
{
    [SerializeField] Inventory storageInventory;
    [SerializeField] InventoryUI storageUI;

    [SerializeField] Transform interactorInventoryPos;
    [SerializeField] int storageAmount = 70;
    
    Inventory interactorInventory;

    public Item testItem;
    public int amount;

    protected override void Update()
    {
        if(interacted && Input.GetKeyDown(KeyCode.Alpha1))
        {
            storageInventory.TryToGetItemFromOtherInventory(testItem, amount, interactorInventory);
        }
    }

    public override void Interact(Charecter charecter)
    {
        base.Interact(charecter);
        if (!isPlayer) { return; }

        if (storageInventory.tag == charecter.tag && charecter != storageInventory.GetOwner())
        {
            storageInventory.ChangeNewUser(charecter,storageInventory);
        }

        interactorInventory = charecter.GetInventory();
        UIManager.instance.ToggleMenu(storageUI.gameObject);
        interactorInventory.ChangeInventoryUIPos(interactorInventoryPos.position);
        storageInventory.ShowSomeSlots(storageAmount);
    }

    public override void StopInteracting()
    {
        interactorInventory.ResetInventoryUIPos();
        UIManager.instance.ToggleMenu(storageUI.gameObject);
        interactorInventory = null;
        base.StopInteracting();
    }
}

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

    void Update()
    {
        if(interacted && Input.GetKeyDown(KeyCode.Escape)&&isPlayer)
        {
            StopInteracting();
        }

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
        interactorInventory.ToggleUI(true);
        interactorInventory.ChangeInventoryUIPos(interactorInventoryPos.position);
        storageUI.gameObject.SetActive(true);
        storageInventory.ShowSomeSlots(storageAmount);
    }

    public override void StopInteracting()
    {
        interactorInventory.ResetInventoryUIPos();
        interactorInventory.ToggleUI(false);
        storageUI.gameObject.SetActive(false);
        interactorInventory = null;
        base.StopInteracting();
    }
}

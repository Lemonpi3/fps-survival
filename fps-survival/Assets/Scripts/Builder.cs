using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : Interactable
{
    
    public BuildingData[] buildings;
    public Transform[] buildPos;
    public BuilderUI builderUI;

    public Player player;
    public Inventory inventory;
    public Inventory alternativeInventory;

    public override void Interact(Charecter charecter)
    {
        base.Interact(charecter);
        player = interactor as Player;
        UIManager.instance.ToggleBuilderUI();
        inventory = player.GetBeaconInventory();
        alternativeInventory = player.GetInventory();
    }

    public override void StopInteracting()
    {
        UIManager.instance.ToggleBuilderUI();
        base.StopInteracting();
    }
}

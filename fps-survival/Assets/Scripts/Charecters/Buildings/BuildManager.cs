using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    Dictionary<Item, int> buildItemCost;

    /// <summary>
    /// Trys to Build
    /// </summary>
    public void BuildBuilding(Inventory aviableInventory, Player player, Transform buildTransform,BuildingData buildingCosts,Inventory alternativeInventory = null)
    {
        buildItemCost = buildingCosts.GetItemList();

        if (CanBuild(aviableInventory, player.playerGold,buildingCosts,player.team))
        {
            foreach (Item item in buildItemCost.Keys)
            {
                aviableInventory.RemoveItem(item, buildItemCost[item]);
            }
            player.modifyGoldAdditive(-buildingCosts.buildGoldCost);

            Building building =Instantiate(buildingCosts.buildingPrefab, buildTransform.position, buildTransform.rotation).GetComponent<Building>(); //SendVillagerToBuildIt
            building.ChangeTeam(player.team);
        }

        if (CanBuild(alternativeInventory, player.playerGold, buildingCosts,player.team))
        {
            foreach (Item item in buildItemCost.Keys)
            {
                alternativeInventory.RemoveItem(item, buildItemCost[item]);
            }
            player.modifyGoldAdditive(-buildingCosts.buildGoldCost);

            Building building = Instantiate(buildingCosts.buildingPrefab, buildTransform.position, buildTransform.rotation).GetComponent<Building>(); //SendVillagerToBuildIt
            building.ChangeTeam(player.team);
        }
    }

    /// <summary>
    /// Checks If resources are aviable to build
    /// </summary>
    public bool CanBuild(Inventory inventory, int goldAmount,BuildingData buildCosts,Team team)
    {
        if (goldAmount < buildCosts.buildGoldCost) { return false; }

        if (team == Team.Team1) { return GameManager.instance.team1Villagers.Count > 0; }

        if (team == Team.Team2) { return GameManager.instance.team2Villagers.Count > 0; }
        
        Dictionary<Item, int> aviableItems = inventory.GetAllItemsAmountFromAnArray(buildCosts.itemsToBuild);

        if(aviableItems == null) { return true; }

        foreach (Item item in buildCosts.itemsToBuild)
        {
            if (aviableItems[item] <= buildItemCost[item])
            {
                return false;
            }
        }

        return true;
    }
}

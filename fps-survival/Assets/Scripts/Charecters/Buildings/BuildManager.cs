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

    public GameObject prebuildPrefab;
    /// <summary>
    /// Trys to Build
    /// </summary>
    public void BuildBuilding(Inventory aviableInventory, Player player, Transform buildTransform, BuildingData buildingData, Inventory alternativeInventory = null)
    {
        buildItemCost = buildingData.GetItemList();
        int villagersNeeded = buildingData.villagersNeeded;

        if (CanBuild(aviableInventory, player.playerGold, buildingData, player.team) && CheckVillagers(player.team, villagersNeeded))
        {
            foreach (Item item in buildItemCost.Keys)
            {
                aviableInventory.RemoveItem(item, buildItemCost[item]);
            }
            player.modifyGoldAdditive(-buildingData.buildGoldCost);

            BuildingBluePrint build = Instantiate(prebuildPrefab, buildTransform).GetComponent<BuildingBluePrint>(); //SendVillagerToBuildIt
            build.InitializeBluePrint(buildingData.buildingPrefab, buildingData.buildTime, player.team, villagersNeeded);
            AssingBuilders(player.team, villagersNeeded, build.gameObject);
        }
        else if (CanBuild(alternativeInventory, player.playerGold, buildingData, player.team)) 
        {
            foreach (Item item in buildItemCost.Keys)
            {
                alternativeInventory.RemoveItem(item, buildItemCost[item]);
            }
            player.modifyGoldAdditive(-buildingData.buildGoldCost);

            BuildingBluePrint build = Instantiate(prebuildPrefab, buildTransform).GetComponent<BuildingBluePrint>(); //SendVillagerToBuildIt
            build.InitializeBluePrint(buildingData.buildingPrefab, buildingData.buildTime, player.team, villagersNeeded);
            AssingBuilders(player.team, villagersNeeded, build.gameObject);
        }
    }

    /// <summary>
    /// Checks If resources are aviable to build
    /// </summary>
    public bool CanBuild(Inventory inventory, int goldAmount, BuildingData buildCosts, Team team)
    {
        if (goldAmount < buildCosts.buildGoldCost) { return false; }

        if (team == Team.Team1) { return GameManager.instance.team1Villagers.Count > 0; }

        if (team == Team.Team2) { return GameManager.instance.team2Villagers.Count > 0; }

        Dictionary<Item, int> aviableItems = inventory.GetAllItemsAmountFromAnArray(buildCosts.itemsToBuild);

        if (aviableItems == null) { return true; }

        foreach (Item item in buildCosts.itemsToBuild)
        {
            if (aviableItems[item] <= buildItemCost[item])
            {
                return false;
            }
        }

        return true;
    }

    public bool CheckVillagers(Team team, int amount)
    {
        int count = 0;
        if (team == Team.Team1)
        {
            foreach (Villager villager in GameManager.instance.team1Villagers)
            {
                if (!villager.hasTaskAssinged)
                {
                    count++;
                }
            }
            if (count >= amount)
            {
                return true;
            }
            else return false;
        }
        else if (team == Team.Team1)
        {
            foreach (Villager villager in GameManager.instance.team2Villagers)
            {
                if (!villager.hasTaskAssinged)
                {
                    count++;
                }
            }
            if (count >= amount)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    void AssingBuilders(Team team, int amount, GameObject sceneBP_GO)
    {
        int count = 0;
        if (team == Team.Team1)
        {
            foreach (Villager villager in GameManager.instance.team1Villagers)
            {
                if (!villager.hasTaskAssinged)
                {
                    count++;
                    if (count >= amount)
                    {
                        break;
                    }
                }
            }
        }
        else
        {
            foreach (Villager villager in GameManager.instance.team2Villagers)
            {
                if (!villager.hasTaskAssinged)
                {
                    count++;
                    villager.Build(sceneBP_GO);
                    if (count >= amount)
                    {
                        break;
                    }
                }
            }
        }
    }
}

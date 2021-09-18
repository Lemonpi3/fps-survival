using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : Charecter
{
    [SerializeField] int rank = -1;
    [SerializeField] int maxRank;

    BuildingData buildingData;

    protected override void Start()
    {
        base.Start();
        buildingData = charecterData as BuildingData;
    }

    public virtual void RankUp(Player player)
    {
        if (CanRankUp(player.playerGold)) 
        {
            rank++;
            _maxHealth += buildingData.rankHpIncrease[rank];
            healthCurrent += buildingData.rankHpIncrease[rank];
            player.modifyGoldAdditive(-buildingData.rankUpGoldCost[rank]);
        }
    }

    bool CanRankUp(int goldAmount)
    {
        if(buildingData.rankUpGoldCost[rank+1] > goldAmount || rank >= maxRank) { return false; }
        return true;
    }
}

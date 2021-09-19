using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Building",menuName ="Buildings/New Building")]
public class BuildingData : CharecterData
{
    [SerializeField] GameObject _buildPrefab;
    public GameObject buildingPrefab => _buildPrefab;

    [SerializeField] int _maxRank;
    public int maxRank => _maxRank;

    [SerializeField] int[] _rankHpIncrease;
    public int[] rankHpIncrease => _rankHpIncrease;

    [SerializeField] int[] _rankUpGoldCost;
    public int[] rankUpGoldCost => _rankUpGoldCost;

    [SerializeField] int _buildCost;
    public int buildGoldCost => _buildCost;

    [SerializeField] float _buildTime;
    public float buildTime => _buildTime;

    [SerializeField] Item[] _itemsToBuild;
    public Item[] itemsToBuild => _itemsToBuild;

    [SerializeField] int[] _itemsToBuildAmountNeeded;
    public int[] itemsToBuildAmountNeeded => _itemsToBuildAmountNeeded;

    Dictionary<Item, int> buildItemList = new Dictionary<Item, int>();

    [SerializeField] int _villagersNeeded;
    public int villagersNeeded => _villagersNeeded;

    public Dictionary<Item ,int> GetItemList(){

        for (int i = 0; i<itemsToBuild.Length; i++)
        {
            buildItemList.Add(itemsToBuild[i], itemsToBuildAmountNeeded[i]);
        }
        return buildItemList;
    }
}

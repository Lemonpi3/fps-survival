using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabern : Building
{
    [SerializeField] int[] _maxFoodStoragePerRank;
    int maxFoodStorage => _maxFoodStoragePerRank[rank+1];

    int currentFoodAmount;

    public void AddFood(int amount)
    {
        currentFoodAmount += amount;
        if(currentFoodAmount > maxFoodStorage) { currentFoodAmount = maxFoodStorage; }
    }

    public int ServeFood(int amount)
    {
        if (currentFoodAmount - amount < 0)
        {
            int leftovers = currentFoodAmount;
            currentFoodAmount = 0;
            return leftovers;
        }
        else 
        {
            currentFoodAmount -= amount;
            return amount;
        } 
    }
}

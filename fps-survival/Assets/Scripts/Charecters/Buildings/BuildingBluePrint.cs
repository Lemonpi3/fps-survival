using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBluePrint : MonoBehaviour
{
    public float buildingTimer;

    public GameObject buildPrefab;

    public List<Villager> builders;
    public int villagersNeeded;
    public Team buildingTeam;

    bool finished => buildingTimer <= 0;

    public void InitializeBluePrint(GameObject buildingPrefab,float buildingTime,Team team,int _villagersNeeded)
    {
        buildPrefab = buildingPrefab;
        buildingTimer = buildingTime;
        buildingTeam = team;
        villagersNeeded = _villagersNeeded;
        StartCoroutine(Building());
    }

    public IEnumerator Building()
    {
        if (finished)
        {
            Building building = Instantiate(buildPrefab,transform).GetComponent<Building>();
            building.ChangeTeam(buildingTeam);
            foreach (Villager villager in builders)
            {
                villager.ModifyFoodAmount((int)villager._maxFood / 5);
                villager.CompleteCurrentTask();
            }
            Destroy(gameObject);
            
        }
        yield return new WaitForSeconds(1);
        if(villagersNeeded <= builders.Count)
        {
            buildingTimer -= 1;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Villager>())
        {
            builders.Add(other.GetComponent<Villager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Villager>())
        {
            builders.Remove(other.GetComponent<Villager>());
        }
    }
}

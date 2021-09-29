using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBluePrint : MonoBehaviour
{
    public float buildingTimer;

    public GameObject buildPrefab;

    public List<Villager> builders;
    int villagersNeeded;

    Team buildingTeam;

    Transform parentTransform;
    bool _building;

    public void InitializeBluePrint(GameObject buildingPrefab,float buildingTime,Team team,int _villagersNeeded)
    {
        buildPrefab = buildingPrefab;
        buildingTimer = buildingTime;
        buildingTeam = team;
        villagersNeeded = _villagersNeeded;
        if (team == Team.Team1)
        {
            parentTransform = BuildManager.instance.team_1_buildingsParent;
        }
        else
            parentTransform = BuildManager.instance.team_2_buildingsParent;
    }

    private void Update()
    {
        if (_building)
        {
            Building();
        }
    }

    public void Building()
    {
        if (villagersNeeded <= builders.Count)
        {
            buildingTimer -= Time.deltaTime;
        }
        if (buildingTimer <= 0)
        {
            foreach (Villager villager in builders)
            {
                villager.ModifyFoodAmount((int)villager._maxFood / 5);
                villager.CompleteCurrentTask();
            }
            Building building = Instantiate(buildPrefab, transform.position, transform.rotation, parentTransform).GetComponent<Building>();
            building.ChangeTeam(buildingTeam);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Villager>())
        {
            builders.Add(other.GetComponent<Villager>());
            if (builders.Count >= villagersNeeded )
            {
                _building = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Villager>())
        {
            builders.Remove(other.GetComponent<Villager>());
            if (builders.Count <= villagersNeeded)
            {
                _building = false;
            }
        }
    }
}

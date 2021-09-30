using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class BasesSpawners : NetworkBehaviour
{
    [SerializeField] GameObject mainBeaconPrefab;
    [SerializeField] GameObject villagePrefab;

    [SerializeField] Transform[] beaconSpawnLocations;
    [SerializeField] Transform[] villageSpawnLocations;

    public void SpawnBeacon(int numberOfTeams)
    {
        List<Transform> locationsUsed = new List<Transform>();

        for (int i = 0; i < numberOfTeams; i++)
        {
            int rng = Random.Range(0, beaconSpawnLocations.Length);
            if (locationsUsed.Count > 0)
            {
                for (int j = 0; j < locationsUsed.Count; j++)
                {
                    while (locationsUsed[j] == beaconSpawnLocations[rng])
                    {
                        rng = Random.Range(0, beaconSpawnLocations.Length);
                    }
                }
            }
            if (i == 0)
            {
                MainBeacon beacon = Instantiate(mainBeaconPrefab, beaconSpawnLocations[rng].position, Quaternion.identity, BuildManager.instance.team_1_buildingsParent).GetComponent<MainBeacon>();
                GameManager.instance.beaconTeam1 = beacon;
            }
            else 
            { 
                MainBeacon beacon = Instantiate(mainBeaconPrefab, beaconSpawnLocations[rng].position, Quaternion.identity, BuildManager.instance.team_2_buildingsParent).GetComponent<MainBeacon>();
                GameManager.instance.beaconTeam2 = beacon;
            }
        }
    }

    public void SpawnVillage()
    {
        int rng = Random.Range(0, villageSpawnLocations.Length);
        GameObject village =Instantiate(villagePrefab, villageSpawnLocations[rng].position, Quaternion.identity);
        GameManager.instance.villagerSpawnPoint = village.transform.Find("VillagerSpawnPoint");
    }
}

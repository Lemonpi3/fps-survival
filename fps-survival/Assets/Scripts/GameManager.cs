using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }else instance = this;
    }

    [Header("GameSettings & current wave")]
    public int currentDay;

    public GameMode gameMode;
    public bool isSurvival;
    [SerializeField] int daysToSurvive = 5;

    [SerializeField] bool _respawnResources = true;
    public bool respawnResources => _respawnResources;

    [SerializeField] Transform _proyectilesParent;
    public Transform proyectilesParent => _proyectilesParent;

    [SerializeField] float _stoppedProyectileLife = 5;
    public float stoppedProyectileLife => _stoppedProyectileLife;

    [Header("Enemies Spawn")]
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    public List<EnemyData> allEnemiesData = new List<EnemyData>();

    public float defaultEnemySpawnRange = 100;

    public int maxEnemyCount = 25;
    public int currentEnemyCount;

    [Header("Respawn Settings")]
    public bool respawnEnabled = true;
    [SerializeField] float baseRespawnTime = 1;
    [SerializeField] float maxRespawnTime = 30;

    [Header("Player lists and respawns")]
    public List<Player> team1Players = new List<Player>();
    public List<Player> team2Players = new List<Player>();

    public bool deathMatchEnabled;

    [SerializeField] int team1Deaths;
    float team1RespawnTime 
    {
        get 
        {
            float _team1RespawnTime = (baseRespawnTime + team1Deaths * currentDay);
            if (_team1RespawnTime > maxRespawnTime)
            {
                return maxRespawnTime;
            }
            return _team1RespawnTime;
        } 
    }

    [SerializeField] int team2Deaths;
    [SerializeField] float team2RespawnTime 
    {
        get
        {
            float _team2RespawnTime = (baseRespawnTime + team2Deaths * currentDay);
            if (team2RespawnTime > maxRespawnTime)
            {
                return maxRespawnTime;
            }
            return _team2RespawnTime;
        } 
    }

    [Header("Beacons")]
    public MainBeacon beaconTeam1;
    public MainBeacon beaconTeam2;

    [Header("Buildings")]
    public List<Building> team1Buildings;
    public List<Building> team2Buildings;

    [Header("Villager Settings")]

    public int maxVillagerCount = 10;
    public GameObject villagerPrefab;
    public Transform villagerSpawnPoint;
    public Transform wildVillagersParent;

    public List<Villager> wildVillagers;
    public List<Villager> team1Villagers;
    public List<Villager> team2Villagers;

    private void Start()
    {
        InitializeStructures();
        RespawnVillagers();
    }

    public void InitializeStructures()
    {
        BasesSpawners basesSpawners = GetComponent<BasesSpawners>();

        basesSpawners.SpawnVillage();
        if (gameMode == GameMode.Single || gameMode == GameMode.CoOp)
        {
            basesSpawners.SpawnBeacon(1);
            beaconTeam1.GetComponent<Charecter>().ChangeTeam(Team.Team1);
        }
        else
        {
            basesSpawners.SpawnBeacon(2);
            beaconTeam2.GetComponent<Charecter>().ChangeTeam(Team.Team2);

        }
    }

    public void GameOver(Team looser=Team.Team1,bool survivalWin = false,bool lostBeacon = false)
    {
        if (gameMode == GameMode.Versus)
        {
            if (isSurvival && beaconTeam1 != null && beaconTeam2 !=null && !deathMatchEnabled && !lostBeacon)
            {
                deathMatchEnabled = true;
                respawnEnabled = false;
                Debug.Log("Respawns disabled");
            }
            else
            if (looser == Team.Team1)
            {
                Debug.Log("Team 2 wins");
            }
            else Debug.Log("Team 1 wins");
        }
        else if (isSurvival)
        {
            if (!survivalWin)
            {
                Debug.Log("GameOver, days left: " + (daysToSurvive - currentDay));
            }
            else Debug.Log("You Survived!!");
        }
        else Debug.Log("GameOver, survived " + currentDay + " days");
    }

    public void NextDay()
    {
        currentDay += 1;
        if(currentDay > daysToSurvive && isSurvival)
        {
            if (gameMode != GameMode.Versus)
            {
                GameOver(Team.Team1, true); //survival win
            }
            else GameOver(Team.Team1, false, true); //enables deathmatch
        }
        if(wildVillagers.Count != maxVillagerCount)
        {
            RespawnVillagers();
            CheckTeamsVillagersFullness();
        }
        if(respawnResources)
        {
            ResourceSpawner.instance.UpdateNodes();
        }
    }

    public void CheckTeamsVillagersFullness()
    {
        if(team1Villagers.Count != 0)
        {
            foreach (Villager villager in team1Villagers)
            {
                villager.CheckFullnessStatus();
            }
        }
        if (team2Villagers.Count != 0)
        {
            foreach (Villager villager in team2Villagers)
            {
                villager.CheckFullnessStatus();
            }
        }
    }

    public void PlayerDied(Team team)
    {
        DeathMatchWin();
        if (team == Team.Team1)
        {
            team1Deaths++;
        }
        else team2Deaths++;
    }

    public void DeathMatchWin()
    {
        if (!deathMatchEnabled) { return; }
        if(team1Players.Count == 0)
        {
            GameOver(Team.Team1);
        }
        if (team2Players.Count == 0)
        {
            GameOver(Team.Team2);
        }
    }

    public float GetRespawnTime(Team team)
    {
        if(team == Team.Team1)
        {
            return team1RespawnTime;
        }
        else return team2RespawnTime;
    }

    public void AddPlayerToTeam(Player player)
    {
        if (player.team == Team.Team1)
        {
            team1Players.Add(player);
        }
        else
        {
            team2Players.Add(player);
        }
        player.SetBeacon();
    }

    public void RespawnVillagers()
    {
        int villagersTotal = wildVillagers.Count + team1Villagers.Count + team2Villagers.Count;
        if (villagersTotal < maxVillagerCount)
        {
            for (int i = 0; i < maxVillagerCount-villagersTotal; i++)
            {
                Instantiate(villagerPrefab, villagerSpawnPoint.position, Quaternion.identity, wildVillagersParent).GetComponent<Villager>();
            }
        }
    }

    public void SpawnEnemies()
    {
        int amountToSpawn = maxEnemyCount - currentEnemyCount;

        if(amountToSpawn == 0) { return; }

        for (int i = 0; i < amountToSpawn; i++)
        {
            foreach(EnemySpawner spawner in enemySpawners)
            {
                spawner.SpawnEnemy();
            }
        }
    }

    public MainBeacon GetBeacon(Team team)
    {
        if(team == Team.Team1)
        {
            return beaconTeam1;
        }
        if (team == Team.Team2)
        {
            return beaconTeam2;
        }
        else return null;
    }

    public Inventory GetBeaconInventory(Team team)
    {
        if (team == Team.Team1)
        {
            return beaconTeam1.GetInventory();
        }
        if (team == Team.Team2)
        {
            return beaconTeam2.GetInventory();
        }
        else return null;
    }

    public Tabern GetTabern(Team team)
    {
        if (team == Team.Team1)
        {
            foreach(Building building in team1Buildings)
            {
                if(building.GetComponent<Tabern>() != null)
                {
                    return building.GetComponent<Tabern>();
                }
            }
            return null;
        }

        if (team == Team.Team2)
        {
            foreach (Building building in team2Buildings)
            {
                if (building.GetComponent<Tabern>() != null)
                {
                    return building.GetComponent<Tabern>();
                }
            }
            return null;
        }

        else return null;
    }
}

public enum GameMode { Single, CoOp, Versus }



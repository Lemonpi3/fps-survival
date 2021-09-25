using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    public GameObject beaconPrefab;

    public MainBeacon beaconTeam1;
    public MainBeacon beaconTeam2;

    [Header("Buildings")]
    public List<Building> team1Buildings;
    public List<Building> team2Buildings;

    [Header("Villager Settings")]

    public int maxVillagerCount = 10;
    public GameObject villagerPrefab;
    public Transform villagerSpawnPoint;

    public List<Villager> wildVillagers;
    public List<Villager> team1Villagers;
    public List<Villager> team2Villagers;

    private void Start()
    {
        beaconTeam1.GetComponent<Charecter>().ChangeTeam(Team.Team1);
        if(beaconTeam2 != null)
        {
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
            RespawnVillager();
            CheckTeamsVillagersFullness();
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

    public void RespawnVillager()
    {
        int villagersTotal = wildVillagers.Count + team1Villagers.Count + team2Villagers.Count;
        if (villagersTotal < maxVillagerCount)
        {
            for (int i = 0; i < maxVillagerCount-villagersTotal; i++)
            {
                Instantiate(villagerPrefab, villagerSpawnPoint.position, Quaternion.identity, villagerSpawnPoint).GetComponent<Villager>();
            }
        }
    }
}

public enum GameMode { Single, CoOp, Versus }



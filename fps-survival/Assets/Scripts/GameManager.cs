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

    GameObject beaconTeam1;
    GameObject beaconTeam2;

    public void GameOver(Team looser=Team.Team1,bool survivalWin = false)
    {
        if (gameMode == GameMode.Versus)
        {
            if (isSurvival && beaconTeam1 != null && beaconTeam2 !=null && !deathMatchEnabled)
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
            GameOver(Team.Team1, true);
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

    public void AddPlayerToTeam(Team team,Player player)
    {
        if (team == Team.Team1)
        {
            team1Players.Add(player);
        }
        else team2Players.Add(player);
    }
}

public enum GameMode { Single, CoOp, Versus }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : Charecter
{
    VillagerData villagerData;

    int maxFullness;
    public int currentFullness;

    int foodAmount;

    [SerializeField]int maxFoodAmount;
    public int _maxFood => maxFoodAmount;

    float foodAmountToStartFeeding;

    public float feedingTime;

    [SerializeField] int maxTasks;
    [SerializeField] Stack<string> taskQue = new Stack<string>();

    public bool hasTaskAssinged => taskQue != null || taskQue.Peek() != "";
    
    VillagerAI villagerAI;
    
    protected override void Start()
    {
        base.Start();
        InitializeVillager();
    }

    //<<<<<<<<<<<<<<<<<<---------Villager Interactions-------->>>>>>>>>>>>>>>>>
    public void FollowPlayer(Player player)
    {
        villagerAI.target = player.gameObject;
        AddTask("Follow");
    }

    //<<<<<<<<<<<<<<<<<<-------------Build Functions---------->>>>>>>>>>>>>>>>>
    public void Build(GameObject scene_blueprintGO)
    {
        villagerAI.target = scene_blueprintGO;
        AddTask("Building");
    }

    //<<<<<<<<<<<<<<<<<<-------------Food Behaivior--------->>>>>>>>>>>>>>>>>>>

    public void CheckFullnessStatus()
    {
        if(currentFullness <= 0)
        {
            AddTask("Flee");  //TrysToRunToVillage
            return;
        }

        Feed();

        float feedStatus = foodAmount / maxFoodAmount;
        if (feedStatus >= 0.7)
        {
            ChangeFullness(1);
        }
        else if (feedStatus >= 0.5 && feedStatus < 0.7)
        {
            return;
        }
        else if (feedStatus >= 0.3 && feedStatus < 0.5)
        {
            int rng = Random.Range(-1, 1);
            ChangeFullness(rng);
        }
        else 
        {
            ChangeFullness(-1);
            TakeHungerDamage(feedStatus);
        }
    }
    public bool hasEaten()
    {
        return foodAmount >= maxFoodAmount;
    }

    public void ChangeFullness(int amount)
    {
        currentFullness += amount;
        if(currentFullness > maxFullness) { currentFullness = maxFullness; }
    }

    public void Feed()
    {
        if (foodAmount <= foodAmountToStartFeeding && !hasTaskAssinged)
        {
           AddTask("Feed");
        }
    }

    public void GetFood(Tabern tabern,int amount)
    {
       foodAmount += tabern.ServeFood(amount);
    }

    public int FoodNeededToBeFull()
    {
        return maxFoodAmount - foodAmount;
    }

    public void TakeHungerDamage(float feedStatus)
    {
        if(feedStatus == 0 && currentFullness != 0)
        {
            int rng = Random.Range(0, 2);
            if(rng == 1)
            {
                TakeDamage((int)_maxHealth / 5);
            }
        }
        if(currentFullness <= 0)
        {
            TakeDamage(_maxHealth); 
        }
    }

    public void ModifyFoodAmount(int amount)
    {
        foodAmount += amount;
    }
    //<<<<<<<<<<<<<<<<<<<<<<<<------------TASK STUFF-------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public string GetCurrentTask()
    {
        if (taskQue.Count == 0)
        {
            return null;
        }
        else return taskQue.Peek();
    }

    public void AddTask(string task)
    {
        if(taskQue.Count >= maxTasks && taskQue.Contains("Building")) { return; }
        taskQue.Push(task);
    }

    public void CompleteCurrentTask()
    {
        taskQue.Pop();
    }

    public void ResetFullness()
    {
        currentFullness = villagerData.SetStartingFullness();
        foodAmount = maxFoodAmount;
        foodAmountToStartFeeding = villagerData.AmountToStartFeeding();
    }

    //<<<<<<<<<<<<<<<<<<<<<------------SETUP & ChangeTeam--------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>
    
    public void InitializeVillager()
    {
        villagerData = charecterData as VillagerData;
        gameObject.name = villagerData.AssingName();
        maxFullness = villagerData._maxFullness;
        AddToVillagerList(Team.Neutral);    //to prevent an NullReference error pop that happens on start only
        villagerAI = GetComponent<VillagerAI>();
    }

    public NpcData GetNpcData()
    {
        return villagerData;
    }

    public override void ChangeTeam(Team newTeam)
    {
        RemoveVillagerFromList();
        base.ChangeTeam(newTeam);
        AddToVillagerList(newTeam);
        villagerAI.ChangeTeam(newTeam);
        ResetFullness();
    }

    void AddToVillagerList(Team team)
    {
        if(team == Team.Team1)
        {
            GameManager.instance.team1Villagers.Add(this);
        }
        if (team == Team.Team2)
        {
            GameManager.instance.team2Villagers.Add(this);
        }
        if(team == Team.Neutral)
        {
            GameManager.instance.wildVillagers.Add(this);
        }
    }

    void RemoveVillagerFromList()
    {
        if (team == Team.Team1)
        {
            GameManager.instance.team1Villagers.Remove(this);
        }
        if (team == Team.Team2)
        {
            GameManager.instance.team2Villagers.Remove(this);
        }
        if (team == Team.Neutral)
        {
            GameManager.instance.wildVillagers.Remove(this);
        }
    }

    public void SetBeacon(MainBeacon _teamBeacon)
    {
        villagerAI.SetPlayerBeacon(_teamBeacon.villagersZone);
    }

    public void SetTabern()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : NpcAI
{
    Tabern tabern;
    Transform villageCenter;
    Transform teamBeacon;

    Villager villager;
    VillagerInteractable villagerInteractable;

    string currentTask;
    float currentSpeed;

    protected override void Start()
    {
        villager = GetComponent<Villager>();
        npcData = villager.GetNpcData();
        villageCenter = GameManager.instance.villagerSpawnPoint;
        villagerInteractable = GetComponentInChildren<VillagerInteractable>();
        base.Start();
        InvokeRepeating("Think", 0, aiTickTime);
    }
       
    private void Think()
    {
        currentTask = villager.GetCurrentTask();
        if (villager.team == Team.Neutral)
        {
            WildVillagerStateMachine();
        }
        else
            TeamVillagerStateMachine();
    }

    void WildVillagerStateMachine()
    {
        switch (currentTask)
        {
            default:
                target = null;
                if (canRoam)
                {
                    Roam();
                }
                if (CalculateDistance(transform.position, villageCenter.position) > roamRange)
                {
                    agent.SetDestination(villageCenter.position);
                }
                break;
            case "Flee":
                if (target != null)
                {
                    agent.SetDestination(-target.transform.position);
                }
                else villager.CompleteCurrentTask();
                break;
            case "Follow":
                if (target != null)
                {
                    currentSpeed = agent.speed;
                    agent.SetDestination(target.transform.position);
                    if(distanceToTar > roamRange)
                    {
                        agent.speed = agent.speed * 2;
                        if(distanceToTar > roamRange * 2)
                        {
                            villager.CompleteCurrentTask();
                        }
                    }
                    else { agent.speed = currentSpeed; }
                    if(teamBeacon != null)
                    {
                        if(CalculateDistance(transform.position, teamBeacon.position)<= roamRange)
                        {
                            villager.ChangeTeam(teamBeacon.GetComponentInParent<MainBeacon>().team);
                            ChangeStartPos(teamBeacon.transform.position);
                            target = null;
                            villager.CompleteCurrentTask();
                        }
                    }
                }
                else villager.CompleteCurrentTask();
                break;
        }
    }

    void TeamVillagerStateMachine()
    {
        if (target != null && reachedTarget) { agent.isStopped = true; return; } else agent.isStopped = false;
        
        switch (currentTask)
        {
            default:
                if (canRoam)
                {
                    Roam();
                }
                if (CalculateDistance(transform.position, teamBeacon.position) > roamRange)
                {
                    agent.SetDestination(teamBeacon.position);
                }
                break;
            case "Flee":
                if(villager.currentFullness <= 0)
                {
                    agent.SetDestination(villageCenter.position);
                    if (CalculateDistance(transform.position, teamBeacon.position) <= roamRange && villager.currentFullness <= 0)
                    {
                        villager.ChangeTeam(Team.Neutral);
                        villagerInteractable.gameObject.SetActive(true);
                        villager.CompleteCurrentTask();
                    }
                }
                else
                {
                    agent.SetDestination(GetRandomRoamPos() * 2);
                    roamTimer -= Time.deltaTime * aiTickTime * 100;

                    if (roamTimer *1.5 <= 0 || agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
                    {
                        villager.CompleteCurrentTask();
                    }
                }
                break;
            case "Feed":
                if(tabern == null) { return; }
                agent.SetDestination(tabern.transform.position);
                if (bumpingWithTarget || CalculateDistance(transform.position, tabern.transform.position) <= stopRange)
                {
                    StartCoroutine(Feed(villager.FoodNeededToBeFull()));
                }
                break;
            case "Building":
                if (target == null) { villager.CompleteCurrentTask(); }

                if (reachedTarget) { return; }

                agent.SetDestination(target.transform.position);
                break;
        }
    }

    IEnumerator Feed(int feedAmount)
    {
        if (villager.hasEaten())
        {
            villager.CompleteCurrentTask();
        }
        yield return new WaitForSeconds(villager.feedingTime);
        villager.GetFood(tabern, feedAmount);
    }

    void ChangeStartPos(Vector3 newStartPos)
    {
        startingPos = newStartPos;
    }

    public void SetPlayerBeacon(Team team)
    {
        teamBeacon = GameManager.instance.GetBeacon(team).transform;
    }

    public void SetTabern(Team team)
    {
        tabern = GameManager.instance.GetTabern(team);
    }

    public void ChangeTeam(Team _team)
    {
        team = _team;
        SetPlayerBeacon(team);
        SetTabern(team);
    }
}

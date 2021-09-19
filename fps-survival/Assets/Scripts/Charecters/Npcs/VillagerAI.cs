using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAI : NpcAI
{
    Tabern tabern;
    Vector3 villageCenter;
    Transform teamBeacon;

    Villager villager;

    string currentTask;
    float currentSpeed;

    protected override void Start()
    {
        villager = GetComponent<Villager>();
        InvokeRepeating("Think", 0, aiTickTime);
        villageCenter = transform.position;
    }

    private void Update()
    {
        
    }

    private void Think()
    {
        currentTask = villager.GetCurrentTask();
        if (team == Team.Neutral)
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
                if (CalculateDistance(transform.position, villageCenter) > roamRange)
                {
                    agent.SetDestination(villageCenter);
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
                            villager.ChangeTeam(teamBeacon.GetComponent<MainBeacon>().team);
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
                    agent.SetDestination(villageCenter);
                    if (CalculateDistance(transform.position, teamBeacon.position) <= roamRange && villager.currentFullness <= 0)
                    {
                        villager.ChangeTeam(Team.Neutral);
                        VillagerInteractable villagerInteractable = GetComponentInChildren<VillagerInteractable>();
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
                agent.SetDestination(tabern.transform.position);
                if (bumpingWithTarget || CalculateDistance(transform.position, tabern.transform.position) <= stopRange)
                {
                    StartCoroutine(Feed(villager.FoodNeededToBeFull()));
                }
                break;
            case "Building":
                if(target == null) { villager.CompleteCurrentTask(); }

                agent.SetDestination(target.transform.position);
                if (reachedTarget)
                {
                    agent.SetDestination(transform.position);
                }
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

    public void SetNewHome(Transform _teamBeacon)
    {
        teamBeacon =  _teamBeacon;
        ChangeStartPos(teamBeacon.position);
    }
}

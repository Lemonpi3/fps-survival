using UnityEngine;
using System;
using System.Collections.Generic;

public class EnemyAI : NpcAI
{
    [SerializeField]
    AIstate aistate = AIstate.Idle;
    
    Enemy enemy;
    Charecter targetChar;

    private enum AIstate { Idle,Moveing,Roaming,Attacking}

    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        InvokeRepeating("StateMachine", 0, aiTickTime);
        if (canRoam)
        {
            aistate = AIstate.Roaming;
        }
        if(target != null)  //if it has ,goes chases a default target 
        {
            aistate = AIstate.Moveing;
            if(target.GetComponent<Charecter>() != null)
            {
                targetChar = target.GetComponent<Charecter>();
            }
        }
    }

    /// <summary>
    /// Checks for a target  while idle or roaming then thinks what to do acording to the situation
    /// </summary>
    private void StateMachine()
    {
        switch (aistate)
        {
            case AIstate.Idle:
                if (target != null)
                {
                    aistate = AIstate.Moveing;
                }
                else UpdateTarget();

                break;

            case AIstate.Moveing:

                StopBumping();
                if (distanceToTar <= sightRange && target != null)
                {
                    agent.SetDestination(target.transform.position);
                    if (distanceToTar <= stopRange)
                    {
                        if (targetChar.team != team || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral)
                        {
                            {
                                 aistate = AIstate.Attacking;
                            }
                        }
                    }
                }
                else
                {
                    target = null;
                    GoToDefaultState();
                }
                break;

            case AIstate.Roaming:
                
                if (target == null)
                {
                    UpdateTarget();
                    Roam();
                }
                else
                {
                    aistate = AIstate.Moveing;
                }
                break;

            case AIstate.Attacking:

                agent.SetDestination(transform.position);
                FaceTarget(target.transform.position);
                enemy.Attacking(targetChar,aiTickTime);

                if (distanceToTar > stopRange && !bumpingWithTarget) // if out of range chases
                {
                    aistate = AIstate.Moveing;
                }
                break;
            default:
                break;

        }
    }

    /// <summary>
    /// Looks for targets in sightRange if there aren't set aistate into roam or idle if it can't roam.
    /// </summary>
    private void UpdateTarget()
    {
        if(target != null) { MoveToPos(target.transform.position);  }

        Charecter[] charecters = FindObjectsOfType<Charecter>();

        float shortestDistance = Mathf.Infinity;
        foreach (Charecter charecter in charecters)
        {
            
            if (charecter.tag != gameObject.tag || charecter.tag !="Friendly" || charecter.tag != "Neutral" && charecter.team != team) 
            {
                float distanceToEnemy = CalculateDistance(transform.position, charecter.transform.position);
                Charecter nearestEnemy;

                if (distanceToEnemy < shortestDistance && charecter != enemy)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = charecter;
                    
                    if (nearestEnemy != null && shortestDistance <= sightRange)
                    {
                        target = charecter.gameObject;
                        targetChar = charecter;
                        aistate = AIstate.Moveing;
                    }
                } 
            }
            else GoToDefaultState();
        }
    }

    

    /// <summary>
    /// if its bumping with the target and can attack it it will switch to attacking else goes to default state
    /// </summary>
    private void StopBumping()
    {
        if (bumpingWithTarget && targetChar != null)
        {
            if (targetChar.team != enemy.team || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral)
            {
                aistate = AIstate.Attacking;
            }
            
        }
        else GoToDefaultState();
    }

    /// <summary>
    /// sets aistate to be either roam or idle
    /// </summary>
    private void GoToDefaultState()
    {
        if(target != null) { return; }

        if (canRoam)
        {
            aistate = AIstate.Roaming;
        }
        else aistate = AIstate.Idle;
    }
   
}

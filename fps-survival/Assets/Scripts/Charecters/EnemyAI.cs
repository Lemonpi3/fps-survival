using UnityEngine;

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
            aistate = AIstate.Idle;
        }
        if(target != null)  //if it has ,goes chases a default target 
        {
            MoveToPos(target.transform.position);
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

                UpdateTarget();
                agent.isStopped = true;
                break;

            case AIstate.Moveing:

                StopBumping();

                if (target != null && CalculateDistance(transform.position, target.transform.position) <= sightRange)
                {
                    agent.Move(target.transform.position); //chase target

                    if (CalculateDistance(transform.position, target.transform.position) <= stopRange && targetChar != null) //if its in attackRange checks if it can be attacked
                    {
                        if (targetChar.team != team || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral)
                        {
                            aistate = AIstate.Attacking;
                        }
                    }
                }
                GoToDefaultState();
                break;

            case AIstate.Roaming:

                Roam();
                UpdateTarget();

                break;
            case AIstate.Attacking:

                if(target == null || CalculateDistance(transform.position,target.transform.position) > sightRange && !bumpingWithTarget) //checks if target is destroyed or if target is too far away
                {
                    GoToDefaultState();
                } 
                else 
                {
                    agent.isStopped = true; //stops to attack

                    FaceTarget(target.transform.position);
                    enemy.Attacking(targetChar);

                    if (CalculateDistance(transform.position, target.transform.position) > stopRange) // if out of range chases
                    {
                        MoveToPos(target.transform.position);
                    }
                }
                break;
            
        }
    }

    /// <summary>
    /// Looks for targets in sightRange if there aren't set aistate into roam or idle if it can't roam.
    /// </summary>
    private void UpdateTarget()
    {
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
                        MoveToPos(target.transform.position);
                    }
                    else GoToDefaultState();
                } 
            }
        }
    }

    protected override void MoveToPos(Vector3 targetPos)
    {
        aistate = AIstate.Moveing;
        base.MoveToPos(targetPos);
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
        target = null;
        if (canRoam)
        {
            aistate = AIstate.Roaming;
        }
        else aistate = AIstate.Idle;
    }
}

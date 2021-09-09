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

        if(target != null)
        {
            MoveToPos(target.transform.position);
            targetChar = target.GetComponent<Charecter>();
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

                if (target != null && CalculateDistance(transform.position, target.transform.position) <= sightRange)
                {
                    agent.Move(target.transform.position); //chase target

                    if (CalculateDistance(transform.position, target.transform.position) <= stopRange && targetChar != null) //if its in attackRange checks if it can be attacked
                    {
                        if(targetChar.team != enemy.team || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral)
                        {
                            aistate = AIstate.Attacking; 
                        }
                    }
                }
                else if (canRoam)
                {
                    aistate = AIstate.Roaming;
                }
                else aistate = AIstate.Idle;
                break;

            case AIstate.Roaming:

                UpdateTarget();
                Roam();

                break;
            case AIstate.Attacking:

                if(target == null || CalculateDistance(transform.position,target.transform.position) > sightRange) //checks if target is destroyed or if target is too far away
                {
                    if (canRoam)
                    {
                        aistate = AIstate.Roaming;
                    }
                    else aistate = AIstate.Idle;
                } 
                else 
                {
                    agent.isStopped = true; //stops to attack

                    FaceTarget(target.transform.position);
                    //Enemy.Attack(targetChar);

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
            if (charecter.team != enemy.team || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral) 
            {
                float distanceToEnemy = CalculateDistance(transform.position, charecter.transform.position);
                Charecter nearestEnemy;

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = charecter;

                    if (nearestEnemy != null && shortestDistance <= sightRange)
                    {
                        target = charecter.gameObject;
                        targetChar = charecter;
                        MoveToPos(target.transform.position);
                    }
                    else if (canRoam)
                    {
                        aistate = AIstate.Roaming;
                    }
                    else aistate = AIstate.Idle;
                } 
            }
        }
    }

    protected override void MoveToPos(Vector3 targetPos)
    {
        aistate = AIstate.Moveing;
        base.MoveToPos(targetPos);
    }
    
}

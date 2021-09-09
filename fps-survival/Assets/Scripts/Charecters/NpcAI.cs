using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NpcAI : MonoBehaviour
{
    //This script contains generic functions that will be used by all other AIs

    public GameObject target;
    public NpcData npcData;

    [SerializeField]
    protected float aiTickTime; //how often will the state machine refresh

    [SerializeField]
    protected float sightRange;

    [SerializeField]
    protected float stopRange; //is also attackRange for enemyAI

    [SerializeField]
    protected bool canRoam;

    [SerializeField]
    protected float roamRange;

    protected Vector3 startingPos;

    private SphereCollider antiBumpCollider; //to prevent the ai bumping while trying to reach the center of big targets with an OnTriggerEnter
    private bool bumpingWithTarget;  //the bool is to control the NpcAI state (specific to their respective AIs (VillagerAI,EnemyAI , etc))
    
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        antiBumpCollider = GetComponent<SphereCollider>();
    }

    protected float CalculateDistance(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1,pos2);
    }

    protected virtual void MoveToPos(Vector3 targetPos)
    {
        agent.SetDestination(targetPos);
        FaceTarget(targetPos);
    }

    /// <summary>
    /// Sends the AI to the startingPos if the distance from startingPos and roamPos is bigger than roamRange
    /// </summary>
    protected void GoToRandomRoamPos()
    {
        float x = Random.Range(-roamRange, roamRange);
        float z = Random.Range(-roamRange, roamRange);
        Vector3 roamPos = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (CalculateDistance(startingPos, roamPos) > roamRange )
        {
            agent.SetDestination(startingPos);
        }
        else agent.SetDestination(roamPos);
    }

    protected void Roam()
    {
        if (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete)
        {
            GoToRandomRoamPos();
        }
    }

    protected virtual void SetupAgent()
    {
        agent.speed = npcData.moveSpeed;

        sightRange = npcData.sightRange;
        stopRange = npcData.attackRange;
        roamRange = npcData.roamRange;

        startingPos = transform.position;
    }

    protected void FaceTarget(Vector3 targetPos)
    {

        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        if (lookRotation == Quaternion.LookRotation(new Vector3(0f, 0f, 0f)))
        {
            transform.rotation = transform.rotation;
        }
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == target)
        {
            bumpingWithTarget = true;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            bumpingWithTarget = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}



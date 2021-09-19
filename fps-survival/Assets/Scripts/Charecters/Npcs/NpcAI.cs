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
    protected float aiTickTime = 0.3f; //how often will the state machine refresh

    [SerializeField]
    protected float sightRange;

    [SerializeField]
    protected float stopRange; //is also attackRange for enemyAI

    [SerializeField]
    protected bool canRoam;

    [SerializeField]
    protected float roamRange;
    [SerializeField]
    protected float roamTimeInterval = 30f;

    protected float distanceToTar;
    protected float roamTimer;
    protected Team team;
    protected Vector3 startingPos;

    private SphereCollider antiBumpCollider; //to prevent the ai bumping while trying to reach the center of big targets with an OnTriggerEnter
    protected bool bumpingWithTarget;  //the bool is to control the NpcAI state (specific to their respective AIs (VillagerAI,EnemyAI , etc))
   
    public bool reachedTarget => distanceToTar <= stopRange || bumpingWithTarget;
    protected NavMeshAgent agent;
    
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        antiBumpCollider = GetComponentInChildren<SphereCollider>();
        SetupAgent();
    }

    protected void FixedUpdate()
    {
        if (target != null)
        {
            distanceToTar = Vector3.Distance(transform.position, target.transform.position);
        }
    }

    public float CalculateDistance(Vector3 pos1, Vector3 pos2)
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
       agent.SetDestination(GetRandomRoamPos());
    }

    protected Vector3 GetRandomRoamPos()
    {
        float x = Random.Range(startingPos.x - roamRange, startingPos.x + roamRange);
        float z = Random.Range(startingPos.z - roamRange, startingPos.z + roamRange);

        Vector3 roamPos = new Vector3(x, transform.position.y, z);
        return roamPos;
    }

    protected void Roam()
    {
        roamTimer -= Time.deltaTime * aiTickTime * 100;
        if (roamTimer <= 0)
        {
            Debug.Log("Roaming");
            if (CalculateDistance(startingPos, transform.position) > roamRange)
            {
                agent.SetDestination(startingPos);
            }
            else
            {
                GoToRandomRoamPos();
            }
            roamTimer = roamTimeInterval;
        }
    }

    protected virtual void SetupAgent()
    {
        agent.speed = npcData.moveSpeed;

        sightRange = npcData.sightRange;
        stopRange = npcData.attackRange;

        roamRange = npcData.roamRange;
        canRoam = npcData.canRoam;

        startingPos = transform.position;
        team = npcData.team;
    }

    protected void FaceTarget(Vector3 targetPos)
    {
        Vector3 direction = new Vector3(targetPos.x - transform.position.x,0,targetPos.z-transform.position.z).normalized;
        
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else 
            transform.rotation = transform.rotation;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startingPos,roamRange);
    }
    
}



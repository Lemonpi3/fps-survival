using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
{
    public GameObject target;
    private Charecter targetChar;
    NavMeshAgent agent;
    Enemy enemy;

    public float sightRange;
    public float attackRange;
    float distance;


    private enum AIstate { Idle, Moveing, Roaming, Attacking }
    [SerializeField] AIstate aistate;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
        }
        StateMachine();
    }

    public void StateMachine()
    {
        switch (aistate)
        {
            case AIstate.Idle:
                if (target != null)
                {
                    aistate = AIstate.Moveing;
                }
                break;
            case AIstate.Moveing:
                if (distance <= sightRange && target != null)
                {
                    agent.SetDestination(target.transform.position);
                    if (distance <= attackRange)
                    {
                        targetChar = target.GetComponent<Charecter>();
                        if (targetChar.team != Team.Enemy || targetChar.team != Team.Friendly || targetChar.team != Team.Neutral)
                        {
                            {
                                Debug.Log("Attacking");
                                aistate = AIstate.Attacking;
                            }
                        }
                    }
                }
                else 
                { 
                    target = null;
                    aistate = AIstate.Idle;
                }
                break;
            case AIstate.Roaming:
                break;
            case AIstate.Attacking:

               // enemy.Attacking(targetChar);
                if (distance > attackRange) // if out of range chases
                {
                    aistate = AIstate.Moveing;
                }
                break;
            default:
                break;
        }
    }
}

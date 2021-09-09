using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Create Charecter/New NPC")]

public class NpcData : CharecterData
{
    [SerializeField]
    private NPC_TYPE _npc_type;
    public NPC_TYPE npc_type => _npc_type;

    [SerializeField]
    private Mesh _npcMesh;
    public Mesh npcMesh => _npcMesh;

    [SerializeField]
    private bool _canRoam;
    public bool canRoam => _canRoam;

    [Header("Npc stats")]
    [SerializeField]
    private float _moveSpeed = 8f;
    public float moveSpeed => _moveSpeed;

    [SerializeField]
    private float _roamRange;
    public float roamRange => _roamRange;

    [SerializeField]
    private float _attackRange;
    public float attackRange => _attackRange; //also counts as stop range for passive npcs

    [Header("Enemy Stats")]
    [SerializeField]
    private int _damage;
    public int damage => _damage;

    [SerializeField]
    private float _attackSpeed;
    public float attackSpeed => _attackSpeed;

    [SerializeField]
    private float _sightRange;
    public float sightRange => _sightRange;
}
public enum NPC_TYPE { Villager,Enemy,Boss}

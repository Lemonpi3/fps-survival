using UnityEngine;
using System;

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

    [SerializeField]
    private bool _dropsLoot;
    public bool dropsLoot => _dropsLoot;

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

    [SerializeField]
    private float _sightRange;
    public float sightRange => _sightRange;

    [Header("Enemy only Stats")]

    [SerializeField]
    private Attack_TYPE _attack_TYPE;
    public Attack_TYPE attack_TYPE => _attack_TYPE;

    [SerializeField]
    private int _damage;
    public int damage => _damage;

    [SerializeField]
    private float _attackSpeed;
    public float attackSpeed => _attackSpeed;

    [SerializeField]
    private GameObject _rangedProyectile;
    public GameObject rangedProyectile => _rangedProyectile;

    [Header("Drop table")]
    [SerializeField]
    private GameObject[] _commonLoot;
    public GameObject[] commonLoot => _commonLoot;

    [SerializeField]
    private GameObject[] _uncommonLoot;
    public GameObject[] uncommonLoot => _uncommonLoot;

    [SerializeField]
    private GameObject[] _rareLoot;
    public GameObject[] rareLoot => _rareLoot;

    [SerializeField]
    private GameObject[] _veryRateLoot;
    public GameObject[] veryRateLoot => _veryRateLoot;
}
public enum NPC_TYPE { Villager,Enemy,Boss}
public enum Attack_TYPE { Melee, Ranged}

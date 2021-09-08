using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Create Charecter/New NPC")]

public class NpcData : CharecterData
{
    [SerializeField]
    private NPC_TYPE _npc_type;
    public NPC_TYPE npc_type => _npc_type;

    [SerializeField]
    private int _damage;
    public int damage => _damage;

    [SerializeField]
    private float _attackSpeed;
    public float attackSpeed => _attackSpeed;

    [SerializeField]
    private float _attackRange;
    public float attackRange => _attackRange;
}
public enum NPC_TYPE { Villager,Enemy,Boss}

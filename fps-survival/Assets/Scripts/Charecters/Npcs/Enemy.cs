using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charecter
{
    public NpcData npcData;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject rangedAttackProyectile;


    Attack_TYPE attack_TYPE;
    EnemyAI enemyAI;
    float timer;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.npcData = npcData;
    }

    protected override void SetCharecterDefaultStats()
    {
        _maxHealth = npcData.healthMax;
        _healthCurrent = _maxHealth;
        team = npcData.team;
        damage = npcData.damage;
        attackSpeed = npcData.attackSpeed;
        attack_TYPE = npcData.attack_TYPE;
    }

    public void Attacking(Charecter target,float aiTickTime)
    {
        timer -= Time.deltaTime * aiTickTime * 100;
        if (timer <= 0)
        {
            Attack(target);
            timer = attackSpeed;
        }
    }

    private void Attack(Charecter target)
    {
        if(attack_TYPE == Attack_TYPE.Melee)
        {
            target.TakeDamage(damage);
        }else
        if (attack_TYPE == Attack_TYPE.Ranged)
        {
            //RangedAttack
        }
    }

    protected override void DropLoot()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charecter
{
    public EnemyData enemyData;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private int damage;

    private GameObject rangedAttackProyectile;


    Attack_TYPE attack_TYPE;
    EnemyAI enemyAI;
    float timer;

    protected override void Start()
    {
        base.Start();
        enemyAI = GetComponent<EnemyAI>();
        enemyAI.npcData = enemyData;
    }

    protected override void SetCharecterDefaultStats()
    {
        _maxHealth = enemyData.healthMax;
        _healthCurrent = _maxHealth;
        team = enemyData.team;
        damage = enemyData.damage;
        attackSpeed = enemyData.attackSpeed;
        attack_TYPE = enemyData.attack_TYPE;
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

    protected override void Die()
    {
        GameManager.instance.currentEnemyCount--;
        DropLoot();
        base.Die();
    }

    protected override void DropLoot()
    {
        enemyData.DropLoot(transform.position);
    }
}

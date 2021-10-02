using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Charecter/New Enemy")]

public class EnemyData : NpcData
{
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

    [Header("LootSettings")]
    [SerializeField]
    private bool _dropsLoot;
    public bool dropsLoot => _dropsLoot;

    [SerializeField, Tooltip("Will drop regardless of rng and loot amount cap")]
    private GameObject[] _garantedLoot;
    public GameObject[] garantedLoot => _garantedLoot;

    [SerializeField, Min(0)] private int[] _minAmountOfgarantedLoot;
    public int[] minAmountOfgarantedLoot => _minAmountOfgarantedLoot;

    [SerializeField, Min(0)] private int[] _maxAmountOfgarantedLoot;
    public int[] maxAmountOfgarantedLoot => _maxAmountOfgarantedLoot;

    [SerializeField, Min(0)] private int _minAmountOfLoot = 0;
    public int minAmountOfLoot => _minAmountOfLoot;

    [SerializeField, Min(0)] private int _maxAmountOfLoot = 5;
    public int maxAmountOfLoot => _maxAmountOfLoot;

    [Header("LootChance")]

    [SerializeField, Range(0, 1)] private float _commonLootChance = 0.5f;
    public float commonLootChance => _commonLootChance;

    [SerializeField, Range(0, 1)] private float _uncommonLootChance = 0.25f;
    public float uncommonLootChance => _uncommonLootChance;

    [SerializeField, Range(0, 1)] private float _rareLootChance = 0.15f;
    public float rareLootChance => _rareLootChance;

    [SerializeField, Range(0, 1)] private float _veryRareLootChance = 0.1f;
    public float veryRareLootChance => _veryRareLootChance;

    [Header("LootTable")]

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
    private GameObject[] _veryRareLoot;
    public GameObject[] veryRareLoot => _veryRareLoot;

    public void DropLoot(Vector3 lootSpawnPoint)
    {
        if (dropsLoot)
        {
            //GarantedLoot
            if(garantedLoot.Length != 0)
            {
                for (int i = 0; i < garantedLoot.Length; i++)
                {
                    int rng = Random.Range(minAmountOfgarantedLoot[i], maxAmountOfgarantedLoot[i] + 1);
                    for (int j = 0; j < rng; j++)
                    {
                        Instantiate(garantedLoot[i], lootSpawnPoint, Quaternion.identity);
                    }
                }
            }

            int lootAmount = Random.Range(minAmountOfLoot, maxAmountOfLoot + 1);

            for (int i = 0; i < lootAmount; i++)
            {

                float lootRNG = Random.Range(0, (commonLootChance + uncommonLootChance + veryRareLootChance + rareLootChance));
                float currentChance = commonLootChance;

                if (lootRNG <= currentChance && commonLoot.Length != 0)
                {
                    int rng2 = Random.Range(0, commonLoot.Length);
                    Instantiate(commonLoot[rng2], lootSpawnPoint, Quaternion.identity);
                    continue;
                }

                currentChance += uncommonLootChance;

                if (lootRNG <= currentChance && uncommonLoot.Length != 0)
                {

                    int rng2 = Random.Range(0, uncommonLoot.Length);
                    Instantiate(uncommonLoot[rng2], lootSpawnPoint, Quaternion.identity);
                    continue;
                }

                currentChance += rareLootChance;

                if (lootRNG <= currentChance && rareLoot.Length != 0)
                {

                    int rng2 = Random.Range(0, rareLoot.Length);
                    Instantiate(rareLoot[rng2], lootSpawnPoint, Quaternion.identity);
                    continue;
                }

                currentChance += veryRareLootChance;

                if (lootRNG <= currentChance && veryRareLoot.Length != 0)
                {
                    int rng2 = Random.Range(0, veryRareLoot.Length);
                    Instantiate(veryRareLoot[rng2], lootSpawnPoint, Quaternion.identity);
                    continue;
                }
            }
        }
    }
}

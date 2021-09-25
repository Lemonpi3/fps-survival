using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Villager", menuName = "Create Charecter/New Villager")]
public class VillagerData : NpcData
{
    [SerializeField, Tooltip("If Charecter Name is empty it chooses from this list")] string[] _names = { "Timmy","Chad","Ricardo Fort","Pepe","Lemon","Banana",
                                                                                                         "Tomato Tom","Sophie","La Jenny","El Brian","Joe Mama","Karen"};

    [Header("Villager starting status traits")]

    [SerializeField, Range(0, 5), Tooltip("max Fullness value of the villager")] int maxFullness = 5;
    public int _maxFullness => maxFullness;

    [SerializeField, Range(1, 5), Tooltip("min Fullness value of the villager")] int minFullness = 1;

    [SerializeField, Range(0, 1), Tooltip("max Fullness value of the villager")] float maxHungerToStartFeeding = 1;
    [SerializeField, Range(0, 1), Tooltip("max Fullness value of the villager")] float minHungerToStartFeeding = 0;


    public int SetStartingFullness()
    {
        return Random.Range(minFullness, maxFullness);
    }

    public string AssingName()
    {
        if (charecterName == "")
        {
            int rng = Random.Range(0, _names.Length);
            return _names[rng];
        }
        else
            return charecterName;
    }

    public float AmountToStartFeeding()
    {
        return Random.Range(minHungerToStartFeeding, maxHungerToStartFeeding);
    }
}

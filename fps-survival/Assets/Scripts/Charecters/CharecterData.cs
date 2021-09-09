using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "Create Charecter/New Charecter")]

public class CharecterData : ScriptableObject
{
    [SerializeField]
    private string _charecterName;
    public string charecterName => _charecterName;

    [SerializeField]
    private Team _team;
    public Team team => _team;

    [Header("Shared stats")]
    [SerializeField]
    private int _healthMax = 1;
    public int healthMax => _healthMax;
}
/// <summary>
/// Hostile: attacks everything even their own , Friendly: Does not attack or get attacked by anyone except hostiles , Team1-2:Player Teams , 
/// Neutral: Does not attack but can get Damage by any , Enemy: Hostile to players.
/// </summary>
public enum Team { Team1,Team2,Neutral,Hostile,Friendly,Enemy}

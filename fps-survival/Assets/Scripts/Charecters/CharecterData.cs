using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New charecter", menuName ="Create Charecter")]

public class CharecterData : ScriptableObject
{
    [SerializeField]
    private int _healthMax;
    public int healthMax => _healthMax;

    [SerializeField]
    private float _moveSpeed;
    public float moveSpeed => _moveSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Items/Resources/New Resource")]
public class Resource : Item
{
    [Header("ResourceNodeSettings")]

    [SerializeField]
    Resource_Type _resource_Type;
    public Resource_Type resource_Type => _resource_Type;

    [SerializeField,Range(0, 4)]
    int _resoruceTier;
    public int resourceTier => _resoruceTier;

    [SerializeField]
    int _resourceAmount;
    public int resourceAmount => _resourceAmount;

    [SerializeField]
    int _respawnTimeDays;
    public int respawnTimeDays => _respawnTimeDays;
}

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

    [Header("GFX Settings")]
    [SerializeField]
    GameObject _GFX;
    public GameObject GFX => _GFX;

    [SerializeField,Min(0), Tooltip("base scale + base scale * random(0,scale)")]
    float _maxHeightScaleVariation = 0.1f;
    public float maxHeightScaleVariation => _maxHeightScaleVariation;

    [SerializeField, Min(0), Tooltip("base scale + base scale * random(0,scale)")]
    float _maxWidthScaleVariation = 0;
    public float maxWidthScaleVariation => _maxWidthScaleVariation;
}
/// <summary>
/// Also holds tool gathering settings
/// </summary>
public enum Resource_Type
{
    Wood, Mineral, Gold, Special, All, None
}
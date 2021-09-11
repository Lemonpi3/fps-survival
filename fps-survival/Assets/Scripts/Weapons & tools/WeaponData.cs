using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon",menuName ="Weapons/New Weapon")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _weaponName;
    public string weaponName => _weaponName;

    [SerializeField]
    private Mesh _mesh;
    public Mesh mesh => _mesh;

    [SerializeField]
    private bool _hasAltFire;
    public bool hasAltFire => _hasAltFire;

    [SerializeField]
    private bool _usesAmmo;
    public bool usesAmmo => _usesAmmo;

    [SerializeField]
    private int _ammoCapacity;
    public int ammoCapacity => _ammoCapacity;

    [SerializeField]
    private float _reloadTime;
    public float reloadTime => _reloadTime;

    [Header("Main fire")]

    [SerializeField]
    private LayerMask _mainAttackMask;
    public LayerMask mainAttackMask => _mainAttackMask;

    [SerializeField]
    private bool _isMainLaser;
    public bool isMainLaser => _isMainLaser;

    [SerializeField]
    private int _mainDamage = 1;
    public int mainDamage => _mainDamage;

    [SerializeField]
    private float _mainAttackSpeed = 1;
    public float mainAttackSpeed => _mainAttackSpeed;

    //Proyectile fire settings
    [SerializeField]
    private GameObject _mainProyectile;
    public GameObject mainProyectile => _mainProyectile;

    [SerializeField]
    private float _mainProyectileSpeed;
    public float mainProyectileSpeed => _mainProyectileSpeed;

    //Laser fire settings
    [SerializeField]
    private GameObject _mainLaser;
    public GameObject mainLaser => _mainLaser;

    [Header("Main fire ResourceGather settings")]

    [SerializeField]
    private Resource_Type _mainCanGather = Resource_Type.None;
    public Resource_Type mainCanGather => _mainCanGather;

    [SerializeField]
    private int _mainGathersUpToResourceTier = 0;
    public int mainGathersUpToResourceTier => _mainGathersUpToResourceTier;

    [SerializeField]
    private int _mainDamageToResources = 1;
    public int mainDamageToResources => _mainDamageToResources;

    [Header("Alt fire")]

    [SerializeField]
    private LayerMask _altAttackMask;
    public LayerMask altAttackMask => _altAttackMask;

    [SerializeField]
    private bool _isAltLaser;
    public bool isAltLaser => _isAltLaser;

    [SerializeField]
    private int _altDamage = 1;
    public int altDamage => _altDamage;

    [SerializeField]
    private float _altAttackSpeed = 1;
    public float altAttackSpeed => _altAttackSpeed;

    //Proyectile fire settings
    [SerializeField]
    private GameObject _altProyectile;
    public GameObject altProyectile => _altProyectile;

    [SerializeField]
    private float _altProyectileSpeed;
    public float altProyectileSpeed => _altProyectileSpeed;

    //Laser fire settings
    [SerializeField]
    private GameObject _altLaser;
    public GameObject altLaser => _altLaser;

    [Header("Alt fire ResourceGather settings")]

    [SerializeField]
    private Resource_Type _altCanGather = Resource_Type.None;
    public Resource_Type altCanGather => _altCanGather;

    [SerializeField]
    private int _altGathersUpToResourceTier = 0;
    public int altGathersUpToResourceTier => _altGathersUpToResourceTier;

    [SerializeField]
    private int _altDamageToResources = 1;
    public int altDamageToResources => _altDamageToResources;
}

/// <summary>
/// Also holds tool gathering settings
/// </summary>
public enum Resource_Type
{
    Wood,Mineral,Gold,Special,All,None
}

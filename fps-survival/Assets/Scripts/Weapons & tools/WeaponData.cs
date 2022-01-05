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
    private GameObject _GFX;
    public GameObject GFX => _GFX;

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

    [SerializeField]
    float _mainRange = 40;
    public float mainRange => _mainRange;

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
    private Resource_Type _mainResType = Resource_Type.None;
    public Resource_Type mainResType => _mainResType;

    [SerializeField]
    private int _mainResTier = 0;
    public int mainResTier => _mainResTier;

    [SerializeField]
    private int _mainResDmg = 1;
    public int mainResDmg => _mainResDmg;

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

    [SerializeField]
    float _altRange = 40;
    public float altRange => _altRange;

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
    private Resource_Type _altResType = Resource_Type.None;
    public Resource_Type altResType => _altResType;

    [SerializeField]
    private int _altResTier = 0;
    public int altResTier => _altResTier;

    [SerializeField]
    private int _altResDmg = 1;
    public int altResDmg => _altResDmg;
}


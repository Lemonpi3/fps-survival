using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note: Proyectile Spawnpoints have to be on the GFX GO as MainProyectileSpawPoint and AltProyectileSpawPoint.

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weapon;
    [SerializeField] GameObject GFX;
    private bool hasAltFire;
    private bool infiniteAmmo;

    private int ammoCapacity;
    private int currentAmmo;
    private float reloadTime;

    //Main fire stats
    private float mainAttackSpeed;

    private bool isMainLaser;
    private Transform mainProyectileSpawnPoint;
    private GameObject mainProyectile;
    private LayerMask mainAttackMask;
    
    private float mainFireTimer;

    //Alt fire stats
    private float altAttackSpeed;

    private bool isAltLaser;
    private Transform altProyectileSpawnPoint;
    private GameObject altProyectile;
    private LayerMask altAttackMask;
    
    private float altFireTimer;

    bool shootingMain;
    bool shootingAlt;
    bool reloading;


    private void Start()
    {
        LoadWeapon();
    }

    public void Update()
    {
        if(_weapon == null) { return; }

        if (shootingMain)
        {
            mainFireTimer -= Time.deltaTime;
            if(mainFireTimer <= 0)
            {
                shootingMain = false;
            }
        }

        if (shootingAlt)
        {
            altFireTimer -= Time.deltaTime;
            if (altFireTimer <= 0)
            {
                shootingAlt = false;
            }
        }

        if (currentAmmo <= 0 && !infiniteAmmo && !reloading )
        {
            StartCoroutine(Reload());
            reloading = true;
        }
    }

    public void ShootMain(Camera cam, Team team)
    {
        if (currentAmmo <= 0 && !infiniteAmmo || shootingMain == true) { return; }

        shootingMain = true;

        if (mainFireTimer <= 0)
        {
            currentAmmo -= 1;

            Proyectile proyectile = Instantiate(mainProyectile, mainProyectileSpawnPoint.position, Quaternion.identity, GameManager.instance.proyectilesParent).GetComponent<Proyectile>();

            proyectile.InitializeProyectile(_weapon.mainDamage, _weapon.mainRange, team,cam.transform, _weapon.mainProyectileSpeed, _weapon.mainResDmg,
                                                    _weapon.mainResTier, _weapon.mainResType);
            mainFireTimer = mainAttackSpeed;
        }
    }

    public void ShootAlt(Camera cam,Team team)
    {
        if (currentAmmo <= 0 && !infiniteAmmo || shootingAlt == true) { return; }

        if(!hasAltFire) { return; } //cancels if weapon doesnt have alt fire

        shootingAlt = true;

        if (altFireTimer <= 0)
        {
            currentAmmo -= 1;

            Proyectile proyectile = Instantiate(altProyectile, altProyectileSpawnPoint.position, Quaternion.identity, transform).GetComponent<Proyectile>();

            proyectile.InitializeProyectile(_weapon.altDamage, _weapon.altRange, team, cam.transform, _weapon.altProyectileSpeed, _weapon.altResDmg,
                                                    _weapon.altResTier, _weapon.altResType);

            altFireTimer = altAttackSpeed;
        }
    }

    public IEnumerator Reload()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoCapacity;
        reloading = false;
        Debug.Log("Reloaded!");
    }

    private void LoadWeapon()
    {
        if(_weapon == null) { return; }

        GFX = Instantiate(_weapon.GFX,transform.position,Quaternion.identity,transform);
        //ammo setup
        infiniteAmmo = !_weapon.usesAmmo;

        if (!infiniteAmmo)
        {
            ammoCapacity = _weapon.ammoCapacity;
            currentAmmo = ammoCapacity;
            reloadTime = _weapon.reloadTime;
        }

        //main fire setup
        mainProyectileSpawnPoint = GFX.transform.Find("MainProyectileSpawPoint");
        mainAttackSpeed = _weapon.mainAttackSpeed;
        mainAttackMask = _weapon.mainAttackMask;
        isMainLaser = _weapon.isMainLaser;

        if (!_weapon.isMainLaser)
        {
            mainProyectile = _weapon.mainProyectile; //SetsUpProyectile
        } 
        else 
        { 
            mainProyectile = Instantiate(_weapon.mainLaser,mainProyectileSpawnPoint.position,Quaternion.identity,transform);  //SetsUpLaser
            mainProyectile.SetActive(false);
        }

        //alt fire setup
        if (_weapon.hasAltFire)
        {
            hasAltFire = _weapon.hasAltFire;
            altProyectileSpawnPoint = GFX.transform.Find("AltProyectileSpawPoint");
            altAttackSpeed = _weapon.altAttackSpeed;
            altAttackMask = _weapon.altAttackMask;
            isAltLaser = _weapon.isAltLaser;

            if (!_weapon.isAltLaser)
            {
                altProyectile = _weapon.altProyectile;  //SetsUpProyectile
            }
            else 
            {
                altProyectile = Instantiate(_weapon.altLaser, altProyectileSpawnPoint.position, Quaternion.identity, transform);  //SetsUpLaser
                altProyectile.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Replaces current weapon with a new one
    /// </summary>
    public void ChangeWeapon(WeaponData weapon)
    {
        _weapon = weapon;
        LoadWeapon();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weapon;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private bool hasAltFire;
    private bool infiniteAmmo;

    private int ammoCapacity;
    private int currentAmmo;
    private float reloadTime;

    //Main fire stats
    private int mainDamage;
    private float mainAttackSpeed;

    private bool isMainLaser;
    private GameObject mainProyectile;
    private float mainProyectileSpeed;
    private LayerMask mainAttackMask;
    
    private Resource_Type mainGatherType;
    private int mainGatherTier;
    private int mainDamageToResource;
    private float mainFireTimer;

    //Alt fire stats
    private int altDamage;
    private float altAttackSpeed;

    private bool isAltLaser;
    private GameObject altProyectile;
    private float altProyectileSpeed;
    private LayerMask altAttackMask;
    
    private Resource_Type altGatherType;
    private int altGatherTier;
    private int altDamageToResource;
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
    public void Shoot(Camera cam,bool altFire = false)
    {
        if(currentAmmo <= 0)
        {
            return;
        }

        RaycastHit _hit;

        if (!altFire)
        {
            shootingMain = true;

            if (mainFireTimer <= 0)
            {
                currentAmmo -= 1;
                
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, mainAttackMask))
                {
                    if (_hit.collider.tag != gameObject.tag)
                    {
                        DamageTarget(_hit.collider);
                    }
                }
                mainFireTimer = mainAttackSpeed;
            }
        }
        else
        {
            shootingAlt = true;

            if (altFireTimer <= 0)
            {
                currentAmmo -= 1;

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, altAttackMask))
                {
                    DamageTarget(_hit.collider, true);
                }
                altFireTimer = altAttackSpeed;
            }
        }
    }

    private void DamageTarget(Collider target,bool altfire = false)
    {
        Charecter _target = target.GetComponent<Charecter>();

        if (!altfire)
        {
            if (target.tag != "Resource" && _target != null)
            {
                Debug.Log(mainDamage);
                    
                _target.TakeDamage(mainDamage);
                    
            }
            else if(target.tag == "Resource")
            {
                Debug.Log(target.GetComponent<Resource>().GatherResource(mainGatherType, mainGatherTier, mainDamageToResource));
            }
        }
        else
        {
            if (target.tag != "Resource" && _target != null)
            {
                _target.TakeDamage(altDamage);
            }

            else if (target.tag == "Resource")
            {
                Debug.Log(target.GetComponent<Resource>().GatherResource(altGatherType, altGatherTier, altDamageToResource));
            }
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

        infiniteAmmo = !_weapon.usesAmmo;
        if (!infiniteAmmo)
        {
            ammoCapacity = _weapon.ammoCapacity;
            currentAmmo = ammoCapacity;
            reloadTime = _weapon.reloadTime;
        }
        //main fire setup
        mainDamage = _weapon.mainDamage;
        mainAttackSpeed = _weapon.mainAttackSpeed;
        mainAttackMask = _weapon.mainAttackMask;
        mainGatherType = _weapon.mainCanGather;
        mainGatherTier = _weapon.mainGathersUpToResourceTier;
        mainDamageToResource = _weapon.mainDamageToResources;

        isMainLaser = _weapon.isMainLaser;

        if (!_weapon.isMainLaser)
        {
            mainProyectile = _weapon.mainProyectile;
            mainProyectileSpeed = _weapon.mainProyectileSpeed;
        } else 
        { 
            mainProyectile = Instantiate(_weapon.mainLaser,bulletSpawnPoint.position,Quaternion.identity,transform); 
        }

        //alt fire setup

        hasAltFire = _weapon.hasAltFire;
       
        if (hasAltFire)
        {
            altDamage = _weapon.altDamage;
            altAttackSpeed = _weapon.altAttackSpeed;
            altAttackMask = _weapon.altAttackMask;
            altGatherType = _weapon.altCanGather;
            altGatherTier = _weapon.altGathersUpToResourceTier;
            altDamageToResource = _weapon.altDamageToResources;

            isAltLaser = _weapon.isAltLaser;

            if (!_weapon.isMainLaser)
            {
                altProyectile = _weapon.altProyectile;
                altProyectileSpeed = _weapon.altProyectileSpeed;
            }
            else 
            { 
                altProyectile = Instantiate(_weapon.altLaser, bulletSpawnPoint.position, Quaternion.identity, transform); 
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

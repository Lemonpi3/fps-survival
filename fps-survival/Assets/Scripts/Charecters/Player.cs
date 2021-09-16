using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Charecter
{
    /*[SerializeField]
    private Inventory inventory;*/
    [SerializeField]
    private int currentWeapon = 0;
    
    [SerializeField]
    private Weapon[] weapons;

    [SerializeField]
    private GameObject weaponsParent;

    Camera cam;

    private void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        LoadWeapons();
        /*if (!IsLocalPlayer)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            Destroy(transform.Find("Camera").gameObject);
            return;
        }*/
        inventory = GetComponent<Inventory>();
        cam = GetComponentInChildren<Camera>();
    }

    protected override void Update()
    {
        base.Update();
        GetInputs();
    }


    protected override void SetCharecterDefaultStats()
    {
        base.SetCharecterDefaultStats();
    }

    protected override void Die()
    {
        base.Die();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }



    private void GetInputs()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            SwapWeapons(currentWeapon + 1);
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            SwapWeapons(currentWeapon - 1);
        }

       /* if (!inventory.isMenuOpen)
        {
            if (Input.GetButton("Fire1"))
            {
                weapons[currentWeapon].Shoot(cam);
            }

            if (Input.GetButton("Fire2"))
            {
                weapons[currentWeapon].Shoot(cam, true);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(weapons[currentWeapon].Reload());
            }
        }*/
        
    }

    private void SwapWeapons(int newSlot)
    {
        weapons[currentWeapon].gameObject.SetActive(false);

        if (newSlot >= weapons.Length)
        {
            currentWeapon = 0;
        }
        else if (newSlot < 0)
        {
            currentWeapon = weapons.Length - 1;
        }
        else
        {
            currentWeapon = newSlot;
        }
        if (weapons[currentWeapon] == null)
        {
            currentWeapon = 0;
        }
        weapons[currentWeapon].gameObject.SetActive(true);
    }

    private void LoadWeapons()
    {
        weaponsParent.tag = team.ToString();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        SwapWeapons(currentWeapon);
    }

    public Collider GetRaycastHit()
    {
        Camera cam = GetComponentInChildren<Camera>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider;
        }
        else return null;
    }
}

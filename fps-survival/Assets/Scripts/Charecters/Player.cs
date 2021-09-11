using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : Charecter
{
    [SerializeField]
    private int currentWeapon = 0;
    [SerializeField]
    private Weapon[] weapons;

    protected override void Start()
    {
        base.Start();
        LoadWeapons();
        /*if (!IsLocalPlayer)
        {
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
            Destroy(transform.Find("Camera").gameObject);
        }*/
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

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(GetRaycastHit());
        }
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

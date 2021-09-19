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

    [SerializeField]
    private GameObject weaponsParent;

    [SerializeField] int _playerGold;
    public int playerGold => _playerGold;

    MainBeacon playerBeacon;
    Transform _respawnPos;
    Camera cam;
    public bool isDead;

    public bool escortingVillager;
    private void Awake()
    {
        
    }

    protected override void Start()
    {
        base.Start();
        GameManager.instance.AddPlayerToTeam(team, this);
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
        GameManager.instance.PlayerDied(team);

        if (!GameManager.instance.respawnEnabled) { Destroy(gameObject); }
        else
        {
            isDead = true;
            StartCoroutine(Respawn(GameManager.instance.GetRespawnTime(team)));
        }
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
    }


    private IEnumerator Respawn(float respawnTime = 0)
    {
        if (isDead)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if(child != cam.gameObject)
                {
                    child.SetActive(!child.activeSelf);
                }
            }
        }

        Debug.Log("respawing");

        yield return new WaitForSeconds(respawnTime);

        transform.position = _respawnPos.position;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child != cam.gameObject)
            {
                child.SetActive(!child.activeSelf);
            }
        }
        isDead = false;
        SetCharecterDefaultStats();
        Debug.Log("respawed");
    }

    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.instance.TogglePlayerInventory_ALL();
        }

        if (isDead) { return; }

        if (!UIManager.instance.isMenuOpen)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                SwapWeapons(currentWeapon + 1);
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                SwapWeapons(currentWeapon - 1);
            }

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit _hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, LayerMask.NameToLayer("Interactable")))
                {
                    _hit.collider.GetComponent<Interactable>().Interact(this);
                }
            }
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

    public void SetRespawnPos(Transform respawnPos)
    {
        _respawnPos.position = respawnPos.position;
    }
    
    /// <summary>
    /// gold += amount
    /// </summary>
    public void modifyGoldAdditive(int amount)
    {
        _playerGold += amount;
    }

    public void SetBeacon()
    {
        if(team == Team.Team1)
        {
            playerBeacon = GameManager.instance.beaconTeam1;
        }
        else
            playerBeacon = GameManager.instance.beaconTeam2;
    }

    public override void ChangeTeam(Team newTeam)
    {
        base.ChangeTeam(newTeam);
        SetBeacon();
    }

    public Inventory GetBeaconInventory()
    {
        return playerBeacon.GetInventory();
    }

    public Transform GetBeaconLocation()
    {
        return playerBeacon.transform;
    }
}

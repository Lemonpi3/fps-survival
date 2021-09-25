using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBeacon : Building
{
    public Transform playerRespawn;
    public Transform villagersZone;
    [SerializeField] StorageChar storage;

    protected override void Start()
    {
        base.Start();
        storage.ChangeTeam(team);
    }

    protected override void Die()
    {
        GameManager.instance.GameOver(team, false, true); //Looses game due to beacon Destruction
        base.Die();
    }
}

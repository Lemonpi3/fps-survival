using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageChar : Charecter
{
    protected override void Start()
    {
        base.Start();
        storageType = StorageType.Storage;
    }

    public override void TakeDamage(int amount)
    {

    }
}

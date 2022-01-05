using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

/// Checks if the gameObject is local
public class LocalCheck : NetworkBehaviour
{
    void Awake()
    {
        if(!NetworkObject.IsLocalPlayer)
        {
            Destroy(gameObject);
        }
    }
}

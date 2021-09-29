using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerInteractable : Interactable
{
    Villager villager;

    private void Start()
    {
        villager = GetComponentInParent<Villager>();
    }

    public override void Interact(Charecter charecter)
    {
        if (charecter.GetComponent<Player>() == null) { return; }
        base.Interact(charecter);
        villager.FollowCharecter(charecter);
        villager.SetBeacon(charecter.team);
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceData resourceData;
    private int maxResourceAmount;
    private int currentAmount;
    //private Item resource;
    private int resourceTier;
    private Resource_Type resource_Type;

    private void Start()
    {
        gameObject.name = resourceData.resourceName;
        maxResourceAmount = resourceData.resAmount;
        currentAmount = maxResourceAmount;
        resource_Type = resourceData.resource_Type;
        resourceTier = resourceData.resTier;
        //respawn set up
    }

    public int GatherResource(Resource_Type toolResourceType,int toolGatherTier,int amountToExtract) //temp
    {
        if (toolResourceType == resource_Type || toolResourceType == Resource_Type.All && toolGatherTier >= resourceTier)
        {
            currentAmount -= amountToExtract + amountToExtract * (toolGatherTier - resourceTier)/2;
            if (currentAmount <= 0)
            {
                Destroy(gameObject, 0.5f);
            }
            return amountToExtract;
        }
        else
            Debug.Log("Tool Cant Gather: " + gameObject.name + " T" + resourceTier + " resourceType: " + resource_Type);
            return 0;
    }
}

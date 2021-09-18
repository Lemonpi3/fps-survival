using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private int maxResourceAmount;
    private int currentAmount;
    //private Item resource;
    [SerializeField] private int resourceTier;
    [SerializeField] private Resource_Type resource_Type;
    [SerializeField] private Item resourceItem;

    private void Start()
    {
        currentAmount = maxResourceAmount;
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

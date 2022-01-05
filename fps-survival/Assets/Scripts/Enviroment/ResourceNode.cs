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

    public void InitializeNode(Resource resItem)
    {
        resource_Type = resItem.resource_Type;
        resourceTier = resItem.resourceTier;
        maxResourceAmount = resItem.resourceAmount;
        currentAmount = maxResourceAmount;
        resourceItem = resItem;
        gameObject.name = resourceItem.itemName;

        Instantiate(resItem.GFX, transform.position,Quaternion.identity, transform);

        float yScale = Random.Range(0, resItem.maxHeightScaleVariation);
        float xScale = Random.Range(0, resItem.maxWidthScaleVariation);

        transform.localScale = new Vector3(transform.localScale.x + transform.localScale.x * xScale,
                                           transform.localScale.y + transform.localScale.y * yScale, 
                                           transform.localScale.z + transform.localScale.z * xScale);
    }

    public Dictionary<Item,int> GatherResource(Resource_Type toolResourceType,int toolGatherTier,int amountToExtract) //temp
    {
        if (toolResourceType == resource_Type || toolResourceType == Resource_Type.All && toolGatherTier >= resourceTier)
        {
            currentAmount -= amountToExtract + amountToExtract * (toolGatherTier - resourceTier)/2;
            int amountToGive = amountToExtract;

            if (currentAmount <= 0)
            {
                amountToGive = amountToExtract + currentAmount;
                Destroy(gameObject, 0.5f);
            }
            Dictionary<Item, int> resToReturn = new Dictionary<Item, int>();
            resToReturn.Add(resourceItem, amountToGive) ;
            return resToReturn;
        }
        else
            Debug.Log("Tool Cant Gather: " + gameObject.name + " T" + resourceTier + " resourceType: " + resource_Type);
        return null;
    }

}

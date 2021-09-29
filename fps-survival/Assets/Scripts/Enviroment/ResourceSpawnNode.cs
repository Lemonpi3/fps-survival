using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawnNode : MonoBehaviour
{
    [SerializeField] GameObject resourceNodePrefab;
    private Resource resource;
    GameObject myResourceNode; //ResourceNodeAttached to this nodespawner
    int daysToRespawn;
    int dayTimer;


    public void SetRes(Resource res)
    {
        resource = res;
        daysToRespawn = res.respawnTimeDays;
        dayTimer = 0;
        ResourceSpawner.instance.allspawnNodes.Add(this);
        gameObject.name = resource.itemName;
    }
    private void Start()
    {
        SphereCollider sphere = GetComponent<SphereCollider>();
        sphere.enabled = false;
    }

    public void UpdateNode()
    {
        if (myResourceNode == null )
        {
            dayTimer--;
            if(dayTimer <= 0)
            {
                ResourceNode node = Instantiate(resourceNodePrefab, transform.position,Quaternion.identity, transform).GetComponent<ResourceNode>();
                node.InitializeNode(resource);
                myResourceNode = node.gameObject;
                dayTimer = daysToRespawn;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Resource")
        {
            if (ResourceSpawner.instance.allspawnNodes.Contains(this))
            {
                ResourceSpawner.instance.allspawnNodes.Remove(this);
                ResourceSpawner.instance.allspawnNodes.TrimExcess();
            }
            Destroy(gameObject);
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the spawn and respawn of a resourceNode
//When the gameObj with this class is initialized it will cast a raycast downwards, if it hits ground it will set its position to the hit pos, if it doesnt it will destroy itself.
//If it doesn't have a resource node it will respawn it after X days(its based on the res)

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
        gameObject.name ="Spawner of " + resource.itemName;
    }

    private void Start()
    {
        SphereCollider sphere = GetComponent<SphereCollider>();
        sphere.enabled = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,Mathf.Infinity))
        {
            if(hit.collider.tag == "Ground")
            {
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
                return;
            }
        }
        RemoveSpawnPoint();
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
            RemoveSpawnPoint();
        }
    }

   public void RemoveSpawnPoint()
   {
        if (ResourceSpawner.instance.allspawnNodes.Contains(this))
        {
            ResourceSpawner.instance.allspawnNodes.Remove(this);
            ResourceSpawner.instance.allspawnNodes.TrimExcess();
        }
        Destroy(gameObject);
   }
}

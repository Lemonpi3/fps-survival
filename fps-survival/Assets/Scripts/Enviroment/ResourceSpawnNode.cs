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
        gameObject.name ="Spawner of " + resource.itemName;
    }

    private void Start()
    {
        SphereCollider sphere = GetComponent<SphereCollider>();
        sphere.enabled = false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
        else RemoveSpawnPoint();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public static ResourceSpawner instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    [SerializeField] int resourceNodesAmount;
    [SerializeField] float spawnRange;
    [SerializeField] float maxHeightSpawn = 100;
    [SerializeField] GameObject spawnNodePrefab;

    [SerializeField] Transform woodParent;
    [SerializeField] Transform stoneParent;

    [Header("Spawn Rates Settings")]
    [SerializeField]
    float T0WeightChance;
    [SerializeField] Resource[] resourcesT0;

    [SerializeField]
    float T1WeightChance;
    [SerializeField] Resource[] resourcesT1;

    [SerializeField]
    float T2WeightChance;
    [SerializeField] Resource[] resourcesT2;

    [SerializeField]
    float T3WeightChance;
    [SerializeField] Resource[] resourcesT3;

    [SerializeField]
    float T4WeightChance;
    [SerializeField] Resource[] resourcesT4;

    public List<ResourceSpawnNode> allspawnNodes = new List<ResourceSpawnNode>();

    private void Start()
    {
        InitializeNodes();
    }

    public void InitializeNodes()
    {
        SpawnNodes(resourceNodesAmount);
        UpdateNodes();
    }

    public void SpawnNodes(int amount)
    {
        float totalChance = T0WeightChance + T1WeightChance + T2WeightChance + T3WeightChance + T4WeightChance;
        for (int i = 0; i < resourceNodesAmount; i++)
        {
            float rng = Random.Range(0, totalChance);
            float xrng = Random.Range(- spawnRange,  + spawnRange);
            float zrng = Random.Range(- spawnRange,  + spawnRange);
            Vector3 nodeSpawnPos = new Vector3(xrng+transform.position.x, maxHeightSpawn, zrng+transform.position.z);

            float currentChance = T0WeightChance;
            //T0
            if (rng < currentChance)
            {
                if (resourcesT0.Length == 0) { continue; }

                int resRng = Random.Range(0, resourcesT0.Length);

                if (resourcesT0[resRng].resource_Type == Resource_Type.Wood)
                {
                    InstantiateRes(nodeSpawnPos, resourcesT0[resRng], woodParent);
                }
                else InstantiateRes(nodeSpawnPos, resourcesT0[resRng], stoneParent);

                continue;
            }
            currentChance += T1WeightChance;
            //T1
            if (rng < currentChance)
            {
                if (resourcesT1.Length == 0) { continue; }

                int resRng = Random.Range(0, resourcesT1.Length);

                if (resourcesT1[resRng].resource_Type == Resource_Type.Wood)
                {
                    InstantiateRes(nodeSpawnPos, resourcesT1[resRng], woodParent);
                }
                else InstantiateRes(nodeSpawnPos, resourcesT1[resRng], stoneParent);

                continue;
            }
            currentChance += T2WeightChance;
            //T2
            if (rng < currentChance)
            {
                if (resourcesT2.Length == 0) { continue; }

                int resRng = Random.Range(0, resourcesT2.Length);

                if (resourcesT2[resRng].resource_Type == Resource_Type.Wood)
                {
                    InstantiateRes(nodeSpawnPos, resourcesT2[resRng], woodParent);
                }
                else InstantiateRes(nodeSpawnPos, resourcesT2[resRng], stoneParent);

                continue;
            }
            currentChance += T3WeightChance;
            //T3
            if (rng < currentChance)
            {
                if (resourcesT3.Length == 0) { continue; }

                int resRng = Random.Range(0, resourcesT3.Length);

                if (resourcesT3[resRng].resource_Type == Resource_Type.Wood)
                {
                    InstantiateRes(nodeSpawnPos, resourcesT3[resRng], woodParent);
                }
                else InstantiateRes(nodeSpawnPos, resourcesT3[resRng], stoneParent);

                continue;
            }
            currentChance += T4WeightChance;
            //T4
            if (rng <= currentChance)
            {
                if (resourcesT4.Length == 0) { continue; }

                int resRng = Random.Range(0, resourcesT4.Length);

                if(resourcesT4[resRng].resource_Type == Resource_Type.Wood)
                {
                    InstantiateRes(nodeSpawnPos, resourcesT4[resRng], woodParent);
                }else InstantiateRes(nodeSpawnPos, resourcesT4[resRng], stoneParent);

                continue;
            }
        }
    }

    public void UpdateNodes()
    {
        foreach(ResourceSpawnNode nodeSpawner in allspawnNodes)
        {
            nodeSpawner.UpdateNode();
        }
    }

    void InstantiateRes(Vector3 nodeSpawnPos,Resource res,Transform parentTransform)
    {
        ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, parentTransform).GetComponent<ResourceSpawnNode>();
        resourceSpawnNode.SetRes(res);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y+(maxHeightSpawn * 0.5f),transform.position.z), new Vector3(spawnRange *2, maxHeightSpawn, spawnRange*2));
    }
}

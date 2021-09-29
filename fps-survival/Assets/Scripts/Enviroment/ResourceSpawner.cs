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
    [SerializeField] GameObject spawnNodePrefab;

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
            float xrng = Random.Range(-transform.position.x - spawnRange, transform.position.x + spawnRange);
            float zrng = Random.Range(-transform.position.z - spawnRange, transform.position.z + spawnRange);
            Vector3 nodeSpawnPos = new Vector3(xrng, 0, zrng);

            float currentChance = T0WeightChance;
            //T0
            if (rng < currentChance)
            {
                if (resourcesT0.Length == 0) { continue; }
                ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, transform).GetComponent<ResourceSpawnNode>();
                int resRng = Random.Range(0, resourcesT0.Length);
                resourceSpawnNode.SetRes(resourcesT0[resRng]);
                continue;
            }
            currentChance += T1WeightChance;
            //T1
            if (rng < currentChance)
            {
                if (resourcesT1.Length == 0) { continue; }
                ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, transform).GetComponent<ResourceSpawnNode>();
                int resRng = Random.Range(0, resourcesT1.Length);
                resourceSpawnNode.SetRes(resourcesT1[resRng]);
                continue;
            }
            currentChance += T2WeightChance;
            //T2
            if (rng < currentChance)
            {
                if (resourcesT2.Length == 0) { continue; }
                ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, transform).GetComponent<ResourceSpawnNode>();
                int resRng = Random.Range(0, resourcesT2.Length);
                resourceSpawnNode.SetRes(resourcesT2[resRng]);
                continue;
            }
            currentChance += T3WeightChance;
            //T3
            if (rng < currentChance)
            {
                if (resourcesT3.Length == 0) { continue; }
                ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, transform).GetComponent<ResourceSpawnNode>();
                int resRng = Random.Range(0, resourcesT3.Length);
                resourceSpawnNode.SetRes(resourcesT3[resRng]);
                continue;
            }
            currentChance += T4WeightChance;
            //T4
            if (rng <= currentChance)
            {
                if (resourcesT4.Length == 0) { continue; }
                ResourceSpawnNode resourceSpawnNode = Instantiate(spawnNodePrefab, nodeSpawnPos, Quaternion.identity, transform).GetComponent<ResourceSpawnNode>();
                int resRng = Random.Range(0, resourcesT4.Length);
                resourceSpawnNode.SetRes(resourcesT4[resRng]);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }
}

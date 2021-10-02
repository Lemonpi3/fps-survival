using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnRange = 100;
    [SerializeField] float minSpawnRange = 10;

    private void Start()
    {
        if(spawnRange <= 0) { spawnRange = GameManager.instance.defaultEnemySpawnRange; }

        GameManager.instance.enemySpawners.Add(this);
    }

    public void SpawnEnemy()
    {
        if(GameManager.instance.allEnemiesData.Count != 0 && GameManager.instance.currentEnemyCount < GameManager.instance.maxEnemyCount)
        {
            int dir = Random.Range(-1, 1);
            if( dir == 0) { dir = 1; }

            float rngX = Random.Range(minSpawnRange, spawnRange)*dir; 
            
            dir = Random.Range(-1, 1);
            if (dir == 0) { dir = 1; }

            float rngZ = Random.Range(minSpawnRange, spawnRange)*dir;

            Vector3 possibleSpawnPos = new Vector3(transform.position.x + rngX, 100, transform.position.z + rngZ);

            RaycastHit hit;
            if (Physics.Raycast(possibleSpawnPos, Vector3.down, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Ground")
                {
                    int rngEnemy = Random.Range(0, GameManager.instance.allEnemiesData.Count);
                    Enemy enemy = Instantiate(enemyPrefab, hit.point, Quaternion.identity).GetComponent<Enemy>();
                    enemy.enemyData = GameManager.instance.allEnemiesData[rngEnemy];
                    GameManager.instance.currentEnemyCount++;
                    return;
                }
            }
            else SpawnEnemy(); //tryAgain
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, spawnRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minSpawnRange);
    }
}

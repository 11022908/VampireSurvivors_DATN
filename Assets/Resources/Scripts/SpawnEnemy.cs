using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float initialDelay;
    public float spawnInterval;
    public int maxEnemyCount;


    public List<Transform> spawnPosition;

    private int enemyCount = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
      
        yield return new WaitForSeconds(initialDelay);

        while (enemyCount < maxEnemyCount)
        {
            Spawn();

            enemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn()
    {
        
        int randomIndex = Random.Range(0, enemyPrefabs.Count);

        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        int index = Random.Range(0, spawnPosition.Count);
        
        Debug.Log("index pos " + $" {index} "+ spawnPosition[index].position);

        Instantiate(enemyPrefab, spawnPosition[index].position, Quaternion.identity);
    }
    
}

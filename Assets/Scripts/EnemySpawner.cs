using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
   
    private float spawnRangeX = 40f;
    private float spawnRangeY = 40f;

    private float startDelay = 2f;
    private float spawnInterval = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnRandomEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
        Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
    }

}


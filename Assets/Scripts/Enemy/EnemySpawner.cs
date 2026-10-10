using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController[] enemyPrefabs;

    [SerializeField] private float spawnRangeX = 40f;
    [SerializeField] private float spawnRangeY = 40f;
    [SerializeField] private PlayerController player;

    private float startDelay = 2f;
    private float spawnInterval = 1.5f;
    private List<EnemyData> enemyDatas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        bool canSpawnEnemies = false;
        enemyDatas = Resources.LoadAll("Enemies", typeof(EnemyData)).Cast<EnemyData>().ToList();
        foreach (EnemyData enemy in enemyDatas)
        {
            if (enemy.requiredLevel == 1)
            {
                canSpawnEnemies = true;
                break;
            }
        }
        if (!canSpawnEnemies)
        {
            Debug.LogError("No valid enemies to spawn. Need at least one Level 1 enemy.");
            return;
        }
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void SpawnRandomEnemy()
    {
        List<EnemyData> validEnemies = enemyDatas.Where(enemyData => enemyData.requiredLevel <= player.GetLevel()).ToList();
        int enemyIndex = Random.Range(0, validEnemies.Count);
        EnemyController enemyPrefab = enemyPrefabs.First(prefab => prefab.GetEnemyType() == validEnemies[enemyIndex].enemyType);//get matching prefab depending on type
        if (enemyPrefab != null)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
            EnemyController spawnedEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemy.GetComponent<EnemyDataManager>().UpdateFromData(validEnemies[enemyIndex]); // set matching enemy data
        }
        else
        {
            Debug.LogError("Couldn't find prefab for enemy type " + validEnemies[enemyIndex].enemyType);
        }
    }
}
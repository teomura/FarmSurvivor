using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController[] enemyPrefabs;

    [SerializeField] private float spawnRangeX = 40f;
    [SerializeField] private float spawnRangeY = 40f;
    [SerializeField] private PlayerController player;
    [SerializeField] private int distance;

    private float startDelay = 3f;
    private float spawnNormal = 2f;
    private float spawnMedium = 1.5f;
    private float spawnHard = 1.0f;
    private float timer;
    private List<EnemyData> enemyDatas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        timer = startDelay;

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
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnNormal);
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (player.GetLevel() > 20)
            {
                timer = spawnMedium;
            }
            else if (player.GetLevel() > 10)
            {
                timer = spawnHard;
            }

            SpawnRandomEnemy();
            timer = spawnNormal;
        }
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

        Vector3 spawnLocation = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
        while (Vector3.Distance(spawnLocation, player.gameObject.transform.position) < distance)
        {
            spawnLocation = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY), 0);
        }
        EnemyController spawn = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
    }
}
using UnityEngine;
using UnityEngine.Pool;
public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Spawn Config")]
    public float minSpawnDistance = 10f;
    public float maxSpawnDistance = 18f;
    public float spawnInterval = 2f;
    public int maxEnemies = 30;

    private float timer;
    private ObjectPool<GameObject> enemyPool;
    private int currentEnemyCount = 0;

    private void Awake()
    {
        enemyPool = new ObjectPool<GameObject>(
            createFunc: () =>
            {
                GameObject enemy = Instantiate(enemyPrefab);
                enemy.GetComponent<PooledEnemy>().SetSpawner(this);
                return enemy;
            },
            actionOnGet: (enemy) =>
            {
                enemy.SetActive(true);
            },
            actionOnRelease: (enemy) =>
            {
                enemy.SetActive(false);
            },
            actionOnDestroy: (enemy) =>
            {
                Destroy(enemy);
            },
            collectionCheck: false,
            defaultCapacity: maxEnemies,
            maxSize: maxEnemies * 2
        );
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPosition = (Vector2)player.position + randomDirection * distance;

        GameObject enemy = enemyPool.Get();
        enemy.transform.position = spawnPosition;
        currentEnemyCount++;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        currentEnemyCount--;
        enemyPool.Release(enemy);
    }
}

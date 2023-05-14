using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies = 5;

    private int currentEnemies = 0;

    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        Instantiate(enemyPrefab, transform.position, transform.rotation);

        currentEnemies++;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}

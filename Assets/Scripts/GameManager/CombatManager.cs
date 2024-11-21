using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners;

    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waveInterval)
        {
            StartNextWave();
            timer = 0f; // Reset timer
        }
    }

    private void StartNextWave()
    {
        waveNumber++;
        foreach (var spawner in enemySpawners)
        {
            spawner.spawnCount += spawner.spawnCountMultiplier;
            spawner.isSpawning = true;
        }
    }
}

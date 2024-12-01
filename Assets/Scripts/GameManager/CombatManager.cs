using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public List<EnemySpawner> enemySpawners;

    public float timer = 0f;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Start()
    {
        // Validasi enemySpawners
        if (enemySpawners == null || enemySpawners.Count == 0)
        {
            Debug.LogError("No EnemySpawners assigned to CombatManager!");
        }
        else
        {
            Debug.Log($"CombatManager initialized with {enemySpawners.Count} spawners.");
        }

        // Mulai gelombang pertama langsung
        StartNextWave();
    }

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
        totalEnemies = 0; // Reset jumlah total musuh untuk gelombang ini

        foreach (var spawner in enemySpawners)
        {
            if (spawner != null)
            {
                spawner.spawnCount += spawner.spawnCountMultiplier;
                spawner.isSpawning = true;
                totalEnemies += spawner.spawnCount; // Tambahkan jumlah musuh yang di-spawn
            }
            else
            {
                Debug.LogWarning("Encountered a null EnemySpawner reference in the list.");
            }
        }

        Debug.Log($"Wave {waveNumber} started with {totalEnemies} enemies.");
    }
}

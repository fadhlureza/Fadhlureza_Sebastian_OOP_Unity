// EnemyTargeting: bergerak menuju ke arah Player dan spawn 3 kapal sekaligus
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;
    public float spawnInterval = 5f; // Interval spawn
    public int maxEnemies = 3; // Jumlah kapal per spawn dan maksimum di layar
    public float spawnRangeX = 5f; // Rentang spawn X

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Hanya instance pertama yang menjalankan spawn berkala
        if (GameObject.FindGameObjectsWithTag("EnemyTargeting").Length <= 1)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Cek jumlah musuh aktif agar tidak melebihi batas
        if (GameObject.FindGameObjectsWithTag("EnemyTargeting").Length < maxEnemies)
        {
            for (int i = 0; i < maxEnemies; i++)
            {
                if (GameObject.FindGameObjectsWithTag("EnemyTargeting").Length < maxEnemies)
                {
                    float spawnPositionX = Random.Range(-spawnRangeX, spawnRangeX);
                    float spawnPositionY = 10f; // Posisi di atas layar

                    // Pastikan musuh tidak menumpuk
                    Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPositionY);
                    if (Physics2D.OverlapCircle(spawnPosition, 0.5f) == null) // Cek jika area spawn kosong
                    {
                        GameObject newEnemy = Instantiate(gameObject, spawnPosition, Quaternion.identity);
                        EnemyTargeting enemyScript = newEnemy.GetComponent<EnemyTargeting>();
                        enemyScript.player = player; // Pastikan setiap instance memiliki referensi ke player
                    }
                }
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Arahkan enemy menuju ke player dan rotasi kepala kapal mengikuti player
            Vector2 direction = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Rotasi mengikuti arah player

            transform.Translate(Vector2.up * speed * Time.deltaTime); // Gerakkan kapal ke depan sesuai rotasi
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) // Pastikan Bullet memiliki tag "Bullet"
        {
            Destroy(collision.gameObject); // Hancurkan bullet
            Destroy(gameObject); // Hancurkan enemy
        }
    }
}
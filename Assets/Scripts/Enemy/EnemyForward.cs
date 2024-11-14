// EnemyForward: bergerak dari atas ke bawah dan memantul ketika mencapai batas layar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 5f; // Kecepatan gerak Enemy
    public float spawnInterval = 5f; // Interval spawn
    public float spawnRangeX = 5f; // Rentang spawn X
    public int maxEnemies = 3; // Maksimum musuh aktif

    private Vector2 direction;

    void Start()
    {
        // Hanya instance pertama yang menjalankan spawn berkala
        if (GameObject.FindGameObjectsWithTag("EnemyForward").Length <= 1)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }

        direction = Vector2.down;
    }

    void SpawnEnemy()
    {
        // Cek jumlah musuh aktif agar tidak melebihi batas
        if (GameObject.FindGameObjectsWithTag("EnemyForward").Length < maxEnemies)
        {
            float spawnPositionX = Random.Range(-spawnRangeX, spawnRangeX);
            float spawnPositionY = 3f;

            GameObject newEnemy = Instantiate(gameObject, new Vector2(spawnPositionX, spawnPositionY), Quaternion.identity);
            EnemyForward enemyScript = newEnemy.GetComponent<EnemyForward>();
            enemyScript.direction = Vector2.down;
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Jika sudah melewati batas layar, ubah arah gerak
        if (transform.position.y < -4.5f || transform.position.y > 5.5f)
        {
            direction = -direction;
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 5f; // Kecepatan gerak Enemy
    public float spawnInterval = 5f; // Interval spawn
    public float spawnRangeY = 5f; // Rentang spawn Y
    public int maxEnemies = 3; // Maksimum musuh aktif

    private Vector2 direction;

    void Start()
    {
        // Hanya instance pertama yang menjalankan spawn berkala
        if (GameObject.FindGameObjectsWithTag("EnemyHorizontal").Length <= 1)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }

        // Tentukan arah gerak berdasarkan posisi awal
        float spawnPositionX = transform.position.x;
        direction = spawnPositionX < 0 ? Vector2.right : Vector2.left;
    }

    void SpawnEnemy()
    {
        // Cek jumlah musuh aktif agar tidak melebihi batas
        if (GameObject.FindGameObjectsWithTag("EnemyHorizontal").Length < maxEnemies)
        {
            // Tentukan posisi spawn acak di kiri atau kanan layar
            float spawnPositionX = Random.value > 0.5f ? -10f : 10f;
            float spawnPositionY = Random.Range(-spawnRangeY, spawnRangeY);

            // Buat instance musuh baru dengan posisi spawn yang ditentukan
            GameObject newEnemy = Instantiate(gameObject, new Vector2(spawnPositionX, spawnPositionY), Quaternion.identity);
            EnemyHorizontal enemyScript = newEnemy.GetComponent<EnemyHorizontal>();

            // Atur arah gerak musuh berdasarkan posisi spawn
            enemyScript.direction = spawnPositionX < 0 ? Vector2.right : Vector2.left;
        }
    }

    void Update()
    {
        // Gerakkan EnemyHorizontal
        transform.Translate(direction * speed * Time.deltaTime);

        // Jika sudah melewati batas layar, ubah arah gerak
        if (Mathf.Abs(transform.position.x) > 10f)
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


// EnemyBoss: bergerak secara horizontal dan memantul ketika mencapai batas layar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 3f;
    public float spawnInterval = 10f; // Interval spawn untuk Boss
    public float spawnRangeY = 5f; // Rentang spawn Y
    public int maxEnemies = 1; // Hanya satu Boss

    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private Vector2 direction;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyBoss").Length <= 1)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }

        direction = Vector2.left;
    }

    void SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyBoss").Length < maxEnemies)
        {
            float spawnPositionX = Random.value > 0.5f ? 10f : -10f;
            float spawnPositionY = Random.Range(-spawnRangeY, spawnRangeY);

            Instantiate(gameObject, new Vector2(spawnPositionX, spawnPositionY), Quaternion.identity);
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Jika sudah melewati batas layar, ubah arah gerak
        if (Mathf.Abs(transform.position.x) > 10f)
        {
            direction = -direction;
        }

        if (Time.time >= nextFireTime)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FireWeapon()
    {
        Debug.Log("Menembak!"); // Tambahkan untuk melihat di konsol
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
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

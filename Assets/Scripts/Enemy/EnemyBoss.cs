using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 5f;         // Kecepatan gerak
    public Weapon weapon;           // Referensi ke weapon
    private Vector3 direction;      // Arah gerak
    private Vector3 spawnPoint;     // Titik spawn
    private float shootCooldown = 2f; // Waktu jeda antar tembakan
    private float shootTimer = 0f;  // Timer untuk menghitung jeda

    void Start()
    {
        // Tentukan posisi spawn dan arah gerak
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (Random.value > 0.5f)
        {
            spawnPoint = new Vector3(-screenBounds.x + 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.left;
        }
        else
        {
            spawnPoint = new Vector3(screenBounds.x - 1, Random.Range(-screenBounds.y + 3f, screenBounds.y), 0);
            direction = Vector3.right;
        }
        transform.position = spawnPoint;

        // Inisialisasi weapon jika belum diset di Inspector
        if (weapon == null)
        {
            weapon = GetComponent<Weapon>();
            if (weapon == null)
            {
                Debug.LogError("Weapon component not found on EnemyBoss!");
            }
        }
    }

    void Update()
    {
        // Gerakkan EnemyBoss
        transform.Translate(direction * speed * Time.deltaTime);

        // Deteksi apakah keluar dari layar dan ubah arah
        Vector3 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x)
        {
            direction = -direction;
        }

        // Perbarui timer dan tembak jika cooldown selesai
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f; // Reset timer setelah menembak
        }
    }

    void Shoot()
    {
        if (weapon != null)
        {
            Debug.Log("EnemyBoss is attempting to shoot."); // Debug untuk cek apakah Shoot dipanggil
            weapon.Shoot();
        }
        else
        {
            Debug.LogWarning("Weapon is null; EnemyBoss cannot shoot.");
        }
    }


}

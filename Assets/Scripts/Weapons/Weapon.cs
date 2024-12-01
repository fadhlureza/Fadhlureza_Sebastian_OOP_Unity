using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bulletPrefab;  // Ganti nama menjadi bulletPrefab untuk menegaskan bahwa ini adalah prefab
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;

    void Awake()
    {
        // Initialize Object Pool untuk bullet
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootIntervalInSeconds)
        {
            Shoot();
            timer = 0f;
        }
    }

    private Bullet CreateBullet()
    {
        // Membuat bullet baru dan mengatur object pool untuk bullet ini
        Bullet newBullet = Instantiate(bulletPrefab);
        newBullet.SetPool(objectPool);
        return newBullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.transform.rotation = bulletSpawnPoint.rotation;

        // Tentukan arah peluru berdasarkan pengguna Weapon
        if (gameObject.CompareTag("EnemyBoss"))
        {
            bullet.transform.up = new Vector3(0, -1, 0);  // Arahkan peluru ke bawah secara manual

        }
        else if (gameObject.CompareTag("Player"))
        {
            bullet.transform.up = transform.up; // Player mengarah ke atas
        }

        bullet.gameObject.SetActive(true);
    }


    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Shoot()
    {
        // Mendapatkan bullet dari pool dan meletakkannya di bulletSpawnPoint
        objectPool.Get();
    }
}

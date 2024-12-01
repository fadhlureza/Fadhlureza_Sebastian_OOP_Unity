using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20f;
    public int damage = 10;

    private Rigidbody2D rb;
    private IObjectPool<Bullet> pool;

    void Awake()
    {
        // Mengambil Rigidbody2D saat pertama kali diinisialisasi
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.velocity = transform.up * bulletSpeed; // Transform.up digunakan untuk arah
    }


    public void SetPool(IObjectPool<Bullet> bulletPool)
    {
        pool = bulletPool;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Misalnya, logika damage
            // collision.GetComponent<Enemy>().TakeDamage(damage);

            // Kembalikan Bullet ke pool daripada menghancurkannya
            pool.Release(this);
        }
    }

    void OnBecameInvisible()
    {
        // Kembalikan Bullet ke pool saat keluar dari layar
        pool.Release(this);
    }
}

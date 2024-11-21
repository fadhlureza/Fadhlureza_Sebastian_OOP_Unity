using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [Header("Max Health")]
    [SerializeField] private float maxHealth = 100f;  // Gunakan SerializeField untuk memberi akses pada Inspector
    [SerializeField] private float health;  // Gunakan SerializeField untuk menampilkan health pada Inspector

    // Getter untuk health
    public float Health
    {
        get { return health; }
    }

    // Constructor (opsional jika ingin menambahkan inisialisasi lebih lanjut)
    void Start()
    {
        // Inisialisasi health dengan maxHealth pada saat object dimulai
        health = maxHealth;
    }

    // Setter untuk mengurangi health
    public void Subtract(float amount)
    {
        health -= amount;

        // Cek jika health kurang dari 0, maka hancurkan objek ini
        if (health <= 0)
        {
            health = 0;  // Pastikan health tidak kurang dari 0
            Destroy(gameObject);  // Menghancurkan objek ini
        }
    }

    // Getter untuk maxHealth
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
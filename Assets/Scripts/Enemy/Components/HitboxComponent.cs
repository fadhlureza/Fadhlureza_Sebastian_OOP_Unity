using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;  // Referensi ke HealthComponent
    private AttackComponent attackComponent;  // Referensi ke AttackComponent

    private Bullet bullet;

    // Pastikan HealthComponent ada pada objek ini dan AttackComponent
    void Start()
    {
        health = GetComponent<HealthComponent>();
        if (health == null)
        {
            Debug.LogError("HealthComponent not found on this GameObject!");
        }
        // Pastikan AttackComponent ada pada objek ini
        attackComponent = GetComponent<AttackComponent>();
        if (attackComponent == null)
        {
            Debug.LogError("AttackComponent not found on this GameObject!");
        }
    }

    // Method untuk menerima damage hanya jika objek tidak sedang invincible
    public void Damage(int damageAmount)
    {
        // Mengecek apakah objek sedang dalam keadaan invincible
        InvincibilityComponent invincibilityComponent = GetComponent<InvincibilityComponent>();
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible)
        {
            invincibilityComponent.StartBlinkingEffect();
            // Jika objek sedang invincible, tidak memberikan damage
            return;
        }

        // Jika objek tidak invincible, kurangi health
        health.Subtract(damageAmount);
    }

    // Overload method Damage untuk menerima Bullet
    public void Damage(Bullet bullet)
    {
        InvincibilityComponent invincibilityComponent = GetComponent<InvincibilityComponent>();
        if (invincibilityComponent != null && !invincibilityComponent.isInvincible)
        {
            invincibilityComponent.StartBlinkingEffect();
            // Jika objek sedang invincible, tidak memberikan damage
            return;
        }

        Damage(bullet.damage);
    }

    // Untuk mendeteksi saat terjadi collision pada Trigger Collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            // Jika tag-nya sama, tidak melakukan damage
            return;
        }
        
        if (other.CompareTag("Enemy"))
        {
            // Jika objek lain memiliki HitboxComponent, kirimkan damage
            HitboxComponent otherHitbox = other.GetComponent<HitboxComponent>();
            if (otherHitbox != null)
            {
                    
                // Menggunakan metode damage dengan damage dari AttackComponent
                otherHitbox.Damage(attackComponent != null ? attackComponent.GetDamage() : 0);
            }
        }
    }
}
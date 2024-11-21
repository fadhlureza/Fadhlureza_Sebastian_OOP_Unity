using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private int damage;
    [SerializeField] public Bullet bullet;

    // Mendapatkan damage dari AttackComponent
    public int GetDamage()
    {
        if (bullet != null)
        {
            damage = bullet.damage;
        }
        return damage;
    }

    // Method untuk menangani colliders dan pengecekan lainnya
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(gameObject.tag))
        {
            // Jika tag-nya sama, tidak melakukan damage
            return;
        }

        // Mengecek apakah objek yang terkena memiliki HitboxComponent
        HitboxComponent hitboxComponent = other.GetComponent<HitboxComponent>();
        if (hitboxComponent != null)
        {
            // Jika objek memiliki HitboxComponent, berikan damage
            hitboxComponent.Damage(damage);
        }
    }
}
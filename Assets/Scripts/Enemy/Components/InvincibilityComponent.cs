using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(HitboxComponent))]
public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Start()
    {
        // Mendapatkan SpriteRenderer yang digunakan untuk mengganti material
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on this GameObject!");
        }

        // Mendapatkan material yang digunakan SpriteRenderer saat ini
        originalMaterial = spriteRenderer.material;
    }

    // Coroutine untuk membuat efek blinking
    private IEnumerator BlinkingRoutine()
    {
        isInvincible = true;  // Set status invincible ke true
        for (int i = 0; i < blinkingCount; i++)
        {
            Debug.Log("Blinking " + i);
            spriteRenderer.material = blinkMaterial;  // Ganti material ke blinkMaterial
            yield return new WaitForSeconds(blinkInterval);

            spriteRenderer.material = originalMaterial;  // Kembalikan material asli
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;  // Set status invincible ke false setelah selesai blinking
    }

    // Method untuk memulai efek blinking dari kelas lain
    public void StartBlinkingEffect()
    {
        if (!isInvincible)
        {
            Debug.Log("Start Blinking Effect");
            StartCoroutine(BlinkingRoutine());
        }
    }
}
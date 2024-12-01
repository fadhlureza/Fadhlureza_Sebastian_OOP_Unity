using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1; // Level default untuk musuh, dapat diatur di Inspector

    void OnDestroy()
    {
        // Cari referensi MainUI
        var mainUI = FindObjectOfType<MainUI>();
        if (mainUI != null)
        {
            // Tambahkan poin ke UI saat musuh mati
            mainUI.AddPoints(level, 1); // Anggap setiap musuh yang mati dihitung sebagai 1
        }
    }

    // You can add more properties and methods here as needed
}
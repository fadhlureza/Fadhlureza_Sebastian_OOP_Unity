using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    public CombatManager combatManager; // Referensi ke CombatManager untuk Wave dan Enemies Left
    private HealthComponent healthComponent; // Referensi ke HealthComponent Player

    private Label healthLabel;        // Label untuk menampilkan Health
    private Label pointsLabel;        // Label untuk menampilkan Points
    private Label waveLabel;          // Label untuk menampilkan Wave
    private Label enemiesLeftLabel;   // Label untuk menampilkan Enemies Left

    private int points = 0;           // Variabel untuk menyimpan poin

    void Start()
    {
        // Cari HealthComponent dari Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            healthComponent = player.GetComponent<HealthComponent>();
        }

        // Ambil referensi ke elemen UI Toolkit
        var uiDocument = GetComponent<UIDocument>();
        var rootVisualElement = uiDocument.rootVisualElement;

        // Ambil elemen UI Label
        healthLabel = rootVisualElement.Q<Label>("Health");
        pointsLabel = rootVisualElement.Q<Label>("Points");
        waveLabel = rootVisualElement.Q<Label>("Wave");
        enemiesLeftLabel = rootVisualElement.Q<Label>("Enemies");

        // Debug jika elemen tidak ditemukan
        if (healthComponent == null) Debug.LogError("HealthComponent not found on Player.");
        if (combatManager == null) Debug.LogError("CombatManager is not assigned.");
        if (healthLabel == null) Debug.LogError("Label 'Health' not found.");
        if (pointsLabel == null) Debug.LogError("Label 'Point' not found.");
        if (waveLabel == null) Debug.LogError("Label 'Wave' not found.");
        if (enemiesLeftLabel == null) Debug.LogError("Label 'Enemies' not found.");

        // Update UI pertama kali
        UpdateUI();
    }

    void Update()
    {
        // Update Health
        if (healthComponent != null && healthLabel != null)
        {
            healthLabel.text = "Health: " + healthComponent.Health.ToString("F0"); // Tanpa desimal
        }
        else if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            // Jika Player sudah dihancurkan
            healthLabel.text = "Health: 0";
        }

        // Update Wave
        if (combatManager != null && waveLabel != null)
        {
            waveLabel.text = "Wave: " + combatManager.waveNumber; // Pastikan waveNumber bersifat public
        }

        // Update Enemies Left
        if (enemiesLeftLabel != null)
        {
            enemiesLeftLabel.text = "Enemies Left: " + CountEnemies();
        }
    }

    // Fungsi untuk menghitung jumlah musuh tersisa di scene
    private int CountEnemies()
    {
        return GameObject.FindObjectsOfType<Enemy>().Length; // Hitung semua musuh berdasarkan kelas Enemy
    }

    // Fungsi untuk memperbarui UI Points
    public void UpdateUI()
    {
        if (pointsLabel != null)
        {
            pointsLabel.text = "Points: " + points;
        }
    }

    // Fungsi untuk menambah poin berdasarkan level dan jumlah musuh yang dihancurkan
    public void AddPoints(int enemyLevel, int enemyCount)
    {
        points += enemyLevel * enemyCount;
        UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder; // Prefab untuk weapon yang terkait dengan pickup ini
    
    private static Weapon currentWeapon; // Weapon yang saat ini dipegang oleh player
    private static WeaponPickup lastPickup; // Referensi ke WeaponPickup terakhir yang diambil

    void Start()
    {
        // Tampilkan visual dari pickup di awal
        TurnVisual(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan objek yang menyentuh trigger memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Hancurkan weapon yang sedang dipegang oleh player jika ada
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            // Instantiate weapon baru dari prefab dan simpan sebagai currentWeapon
            currentWeapon = Instantiate(weaponHolder, other.transform);

            // Set posisi lokal weapon agar berada di tengah player
            currentWeapon.transform.localPosition = Vector3.zero;

            // Sembunyikan visual dari pickup ini setelah diambil
            TurnVisual(false);

            // Nonaktifkan collider agar pickup ini tidak bisa diambil lagi
            GetComponent<Collider2D>().enabled = false;

            // Jika ada pickup terakhir, aktifkan kembali pickup tersebut
            if (lastPickup != null && lastPickup != this)
            {
                lastPickup.ResetPickup();
            }

            // Simpan referensi pickup ini sebagai lastPickup
            lastPickup = this;

            Debug.Log("Weapon has been picked up by the player.");
        }
    }

    // Fungsi untuk menampilkan atau menyembunyikan visual dari WeaponPickup
    public void TurnVisual(bool on)
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = on;
        }
    }

    // Fungsi untuk mengaktifkan kembali pickup ini
    public void ResetPickup()
    {
        // Aktifkan visual dan collider pickup ini kembali
        TurnVisual(true);
        GetComponent<Collider2D>().enabled = true;
    }
}



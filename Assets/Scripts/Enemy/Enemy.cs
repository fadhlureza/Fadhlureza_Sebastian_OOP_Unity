using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level = 1; // Level setiap Enemy, bisa disesuaikan

    void Start()
    {
        // Atur posisi Enemy menghadap ke Player (anggap Player berada pada posisi default)
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}



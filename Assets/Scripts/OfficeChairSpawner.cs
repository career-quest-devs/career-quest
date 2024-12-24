using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeChairSpawner : MonoBehaviour
{
    public GameObject chairPrefab; // Assign the office chair prefab
    public float spawnInterval = 5.0f; // Time between spawns

    private float timer;
    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f; // Reset timer to avoid instant spawn
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnChair();
            timer = 0f;
        }
    }

    private void SpawnChair()
    {
        // Instantiate the chair prefab
        GameObject chair = Instantiate(chairPrefab, transform.position, Quaternion.identity);
    }
}

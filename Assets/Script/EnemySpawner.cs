using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    
    private Camera mainCamera;
    private float timeSinceLastSpawn;
    private float screenWidthInWorld;
    private float spawnY;
    
    void Start()
    {
        mainCamera = Camera.main;
        
        // Calculate spawn position at the top of the screen
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 topLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        
        screenWidthInWorld = topRight.x - topLeft.x;
        spawnY = topRight.y;
        
        // Start spawning
        timeSinceLastSpawn = spawnRate;
    }
    
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        
        if (timeSinceLastSpawn >= spawnRate)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }
    
    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is not assigned!");
            return;
        }
        
        // Calculate a random X position within the screen width
        float xPadding = 0.5f; // Adjust based on your enemy's size
        float randomX = Random.Range(-screenWidthInWorld/2 + xPadding, screenWidthInWorld/2 - xPadding);
        
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);
        
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
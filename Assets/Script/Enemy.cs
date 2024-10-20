using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int health = 2;
    public int pointValue = 100;
    public GameObject powerUpPrefab;
    public GameObject bulletPrefab;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    private Camera mainCamera;
    private float minY;
    
    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
        
        // Calculate the bottom of the screen in world coordinates
        minY = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
    }
    
    void Update()
    {
        // Move enemy downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        // Check if enemy is below the bottom of the screen
        if (transform.position.y < minY)
        {
            Debug.Log("Enemy destroyed - off screen");
            Destroy(gameObject);
            return;
        }
        
        // Shooting logic
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.down * 0.5f, Quaternion.identity);
            bullet.GetComponent<Bullet>().isEnemyBullet = true;
        }
    }
    
    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            GameManager.instance.AddScore(pointValue);

            if (Random.value < 0.2f && powerUpPrefab != null)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
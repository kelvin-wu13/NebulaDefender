using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public bool isEnemyBullet = false;

    private Camera mainCamera;
    private float minY, maxY;
    
    void Start()
    {
        mainCamera = Camera.main;
        
        // Calculate the top and bottom of the screen in world coordinates
        Vector3 bottomScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        minY = bottomScreen.y;
        maxY = topScreen.y;
    }
    
    void Update()
    {
        // Move bullet
        Vector2 direction = isEnemyBullet ? Vector2.down : Vector2.up;
        transform.Translate(direction * speed * Time.deltaTime);
        
        // Check if bullet is off screen
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            Debug.Log("Bullet destroyed - off screen");
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemyBullet)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
        else  // Player bullet
        {
            if (other.CompareTag("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
                Destroy(gameObject);
            }
        }
    }
}
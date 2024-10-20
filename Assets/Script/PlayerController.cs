using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int health = 3;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    
    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement * speed * Time.deltaTime);
        
        // Keep player on screen
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -12f, 11f);
        position.y = Mathf.Clamp(position.y, -5f, 7f);
        transform.position = position;
        
        // Auto shooting
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    
    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
    }
    
    public void TakeDamage()
    {
        health--;
        GameManager.instance.TakeDamage(1);
        
        if (health <= 0)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);
        }
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 3f;
    public float duration = 5f;
    
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Double fire rate
                player.fireRate /= 2f;
                
                // Reset fire rate after duration
                Invoke("EndPowerUp", duration);
            }
            
            Destroy(gameObject);
        }
    }
    
    void EndPowerUp()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.fireRate *= 2f;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public static CameraBounds instance;
    
    [HideInInspector]
    public float minY, maxY, minX, maxX;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CalculateBounds();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void CalculateBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        
        minX = bottomLeft.x;
        minY = bottomLeft.y;
        maxX = topRight.x;
        maxY = topRight.y;
    }
    
    public bool IsOffScreen(Vector3 position)
    {
        return position.y < minY || position.y > maxY || 
               position.x < minX || position.x > maxX;
    }
}

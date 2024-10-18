using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinMovement : MonoBehaviour
{
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        // Move the pin horizontally
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
        // Destroy the pin if it goes off-screen
        if (transform.position.x > Screen.width) 
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombMoving : MonoBehaviour
{
    [SerializeField]public float speed = 3.0f; // Speed of the bomb
    [SerializeField]private Vector2 direction; // Direction of movement
    // Start is called before the first frame update
    void Start()
    {
        // Set direction to only move up or down
        direction = new Vector2(0, Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bomb
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the bomb goes beyond the screen edges
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.y > 1)
        {
            // Reverse the direction
            direction = new Vector2(0, -direction.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pin"))
        {
            Debug.Log("Pin hit a bomb! Restarting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
        }
    }
}

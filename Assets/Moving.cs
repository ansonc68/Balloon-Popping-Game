using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    [SerializeField]public float speed = 2.0f; // Speed of the balloon
    [SerializeField]private Vector2 direction; // Direction of movement
    [SerializeField]private AudioSource audioSource;
    [SerializeField]public ScoreKeeper scoreKeeper; 

    void Start()
    {
        // Set direction to only move up or down
        direction = new Vector2(0, Random.Range(-1f, 1f)).normalized;
        if (audioSource == null){
            audioSource = GetComponent<AudioSource>();
        }
        InvokeRepeating("GrowBalloon", 0f, 2f); // Grow every 2 seconds
    }

    void Update()
    {
        // Move the balloon
        transform.Translate(direction * speed * Time.deltaTime);

        // Check if the balloon goes beyond the screen edges
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.y < 0 || screenPosition.y > 1)
        {
            // Reverse the direction
            direction = new Vector2(0, -direction.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision!" + other);
        if (other.gameObject.tag == "Pin")
        {
            Debug.Log("collided with balloon");
            AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
            float sizeFactor = transform.localScale.x; // Assuming uniform scaling
            int points = sizeFactor < 0.4f ? 10 : sizeFactor < 0.7f ? 5 : 0; // Scoring logic
            scoreKeeper.AddPoints(points);
            Destroy(other.gameObject);
            Destroy(gameObject); 
        }
        else
            Debug.Log("Collided with something else");
    }

    void GrowBalloon()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0); // Increase size by 10%
        if (transform.localScale.x >= 1.2f) // Adjust size limit as needed
        {
            Destroy(gameObject); // Balloon disappears
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart the current scene
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjects : MonoBehaviour
{
    public int points;
    [SerializeField] private AudioClip clip;

    // Increase the score when the player collides with this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Increase the score by 1 (you can adjust this as needed)
            GameManager.UpdateScore(points, clip);

            gameObject.SetActive(false);
        }
    }
}

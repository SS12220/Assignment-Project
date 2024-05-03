using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public Transform pickPointsParent;
    public Transform spawnObjectsParent;

    private List<Transform> pickPoints = new List<Transform>();
    private List<int> usedIndices = new List<int>();

    private void Start()
    {
        // Populate list with pick points
        foreach (Transform child in pickPointsParent)
        {
            pickPoints.Add(child);
        }

        // Spawn objects at random pick points
        foreach (Transform spawnObject in spawnObjectsParent)
        {
            int randomIndex = GetUnusedRandomIndex();
            if (randomIndex != -1)
            {
                spawnObject.position = pickPoints[randomIndex].position;
                usedIndices.Add(randomIndex);
            }
            else
            {
                Debug.LogError("Not enough pickup points for spawning objects.");
                break;
            }
        }
    }

    // Get a random index that hasn't been used before
    private int GetUnusedRandomIndex()
    {
        int randomIndex = Random.Range(0, pickPoints.Count);
        int originalIndex = randomIndex;
        while (usedIndices.Contains(randomIndex))
        {
            randomIndex = (randomIndex + 1) % pickPoints.Count; // Move to next index, wrapping around if necessary
            if (randomIndex == originalIndex) // All indices have been used
                return -1;
        }
        return randomIndex;
    }
}

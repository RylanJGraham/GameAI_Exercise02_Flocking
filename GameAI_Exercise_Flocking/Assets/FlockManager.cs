using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public GameObject birdPrefab;
    public int numberOfBirds = 10;
    public float spawnHeight = 10.0f; // Height above the plane.
    public float spawnRadius = 25.0f; // Radius for random bird spawning.
    
    private void Start()
    {
        for (int i = 0; i < numberOfBirds; i++)
        {
            // Randomize the X and Z position within the specified range (25x25 centered).
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);
            Vector3 randomPosition = new Vector3(randomX, spawnHeight, randomZ);

            Instantiate(birdPrefab, randomPosition, Quaternion.identity);
        }
    }
}

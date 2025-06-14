using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rock1;
    public GameObject rock2;
    public GameObject rock3;
    public GameObject rock4;
    public GameObject[] rockVariants; // Assign your rock variants in the Inspector (little_stone_01, etc.)
    public int numberOfRocks = 100; // Total number of rocks to spawn
    public Vector2 mapBounds = new Vector2(500f, 500f); // Size of the map (X and Z boundaries)
    public float centerRadius = 50f; // Rocks will not spawn within this radius from the center (0, 0)
    public float minRockScale = 0.5f; // Minimum size of rocks
    public float maxRockScale = 2.0f; // Maximum size of rocks

    void Start()
    {
        // Initialize rock variants array
        rockVariants = new GameObject[] { rock1, rock2, rock3, rock4 };
        
        // Spawn the rocks
        for (int i = 0; i < numberOfRocks; i++)
        {
            SpawnRandomRock();
        }
    }

    void SpawnRandomRock()
    {
        Vector3 randomPosition;

        // Generate random positions until one is found outside the center radius
        do
        {
            // Randomly position the rock within map bounds
            randomPosition = new Vector3(
                Random.Range(-mapBounds.x, mapBounds.x),
                0, // Y will be adjusted to the terrain height later
                Random.Range(-mapBounds.y, mapBounds.y)
            );
        } 
        while (randomPosition.magnitude <= centerRadius); // Check if the position is outside the center radius

        // Adjust Y position based on terrain height
        float terrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition);
        randomPosition.y = terrainHeight;

        // Randomly pick a rock variant
        GameObject rockPrefab = rockVariants[Random.Range(0, rockVariants.Length)];

        // Instantiate the selected rock prefab
        GameObject newRock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);

        // Randomize the size of the rock
        float randomScale = Random.Range(minRockScale, maxRockScale);
        newRock.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // Optionally rotate the rock randomly for variation
        newRock.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}

using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public GameObject treePrefab; // Assign your tree prefab in the Inspector
    public int numberOfClusters = 10; // Number of tree clusters
    public int treesPerCluster = 20; // Number of trees per cluster
    public float clusterRadius = 50f; // Radius for each cluster
    public Vector2 mapBounds = new Vector2(500f, 500f); // Size of the map
    public float minTreeScale = 0.5f; // Minimum size of the trees
    public float maxTreeScale = 2.0f; // Maximum size of the trees
    public float trunkBuryingFactor = 0.5f; // Factor to bury the tree trunk (e.g., 0.5 means half-buried)

    // Max terrain height where trees can be generated (can be adjusted based on your terrain settings)
    public float maxTerrainHeight = 100f;

    void Start()
    {
        // Spawn the tree clusters
        for (int i = 0; i < numberOfClusters; i++)
        {
            SpawnTreeCluster();
        }
    }

    void SpawnTreeCluster()
    {
        // Randomly pick a cluster center within the map bounds
        Vector3 clusterCenter = new Vector3(
            Random.Range(-mapBounds.x, mapBounds.x),
            0, // Y will be adjusted to the terrain height later
            Random.Range(-mapBounds.y, mapBounds.y)
        );

        // Get terrain height at the cluster center and bias it towards lower areas (closer to y=0)
        float terrainHeight = GetBiasedTerrainHeight(clusterCenter);
        clusterCenter.y = terrainHeight;

        // Spawn trees within the cluster
        for (int i = 0; i < treesPerCluster; i++)
        {
            // Randomly position the tree around the cluster center
            Vector3 randomPosition = clusterCenter + new Vector3(
                Random.Range(-clusterRadius, clusterRadius),
                0,
                Random.Range(-clusterRadius, clusterRadius)
            );

            // Adjust Y position based on terrain height
            float treeTerrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition);
            randomPosition.y = treeTerrainHeight;

            // Instantiate the tree prefab
            GameObject newTree = Instantiate(treePrefab, randomPosition, Quaternion.identity);

            // Randomize the size of the tree
            float randomScale = Random.Range(minTreeScale, maxTreeScale);
            newTree.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Partially bury the tree trunk by adjusting the Y position
            newTree.transform.position = new Vector3(
                randomPosition.x,
                randomPosition.y - (newTree.transform.localScale.y * trunkBuryingFactor),
                randomPosition.z
            );
        }
    }

    // Function to bias terrain height towards low Y-values
    float GetBiasedTerrainHeight(Vector3 position)
    {
        float height = Terrain.activeTerrain.SampleHeight(position);
        
        // Bias height: More probability to stay closer to 0 height
        // You can adjust this function to tune how close to y=0 the clusters will spawn
        float biasFactor = Mathf.Pow(1 - (height / maxTerrainHeight), 3); // Cubic biasing
        
        return height * biasFactor;
    }
}

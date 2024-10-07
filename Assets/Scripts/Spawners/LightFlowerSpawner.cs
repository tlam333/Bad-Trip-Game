using UnityEngine;

public class LightFlowerSpawner : MonoBehaviour
{
    public GameObject lightFlowerPrefab; // Assign the light_flower prefab in the Inspector
    public int flowersInCenter = 10; // Number of flowers to spawn near the center
    public int flowersOutsideCenter = 20; // Number of flowers to spawn outside the center
    public float centerRadius = 50f; // Defines the radius of the center area
    public Vector2 mapBounds = new Vector2(500f, 500f); // Size of the map (X and Z boundaries)
    public int clusterSize = 5; // Number of flowers per cluster
    public float clusterRadius = 20f; // Radius of each cluster
    public float minLightIntensity = 1f; // Minimum intensity for the light
    public float maxLightIntensity = 4f; // Maximum intensity for the light
    public float minScale = 0.5f; // Minimum scale for the flowers
    public float maxScale = 2.0f; // Maximum scale for the flowers

    void Start()
    {
        // Spawn flowers in the center of the map
        SpawnFlowerClustersInCenter();
        
        // Spawn flowers outside the center area
        SpawnFlowerClustersOutsideCenter();
    }

    void SpawnFlowerClustersInCenter()
    {
        for (int i = 0; i < flowersInCenter / clusterSize; i++) // Divide by clusterSize to spawn clusters
        {
            // Random center point within the center radius for the cluster
            Vector2 randomClusterPosition2D = Random.insideUnitCircle * centerRadius;
            Vector3 clusterCenter = new Vector3(randomClusterPosition2D.x, 0, randomClusterPosition2D.y);

            // Adjust Y position based on terrain height
            float terrainHeight = Terrain.activeTerrain.SampleHeight(clusterCenter);
            clusterCenter.y = terrainHeight;

            // Spawn individual flowers in the cluster
            SpawnFlowersInCluster(clusterCenter);
        }
    }

    void SpawnFlowerClustersOutsideCenter()
    {
        for (int i = 0; i < flowersOutsideCenter / clusterSize; i++) // Divide by clusterSize to spawn clusters
        {
            Vector3 clusterCenter;

            do
            {
                // Random cluster center point within map bounds
                clusterCenter = new Vector3(
                    Random.Range(-mapBounds.x, mapBounds.x),
                    0,
                    Random.Range(-mapBounds.y, mapBounds.y)
                );
            }
            while (clusterCenter.magnitude <= centerRadius); // Ensure it's outside the center area

            // Adjust Y position based on terrain height
            float terrainHeight = Terrain.activeTerrain.SampleHeight(clusterCenter);
            clusterCenter.y = terrainHeight;

            // Spawn individual flowers in the cluster
            SpawnFlowersInCluster(clusterCenter);
        }
    }

    void SpawnFlowersInCluster(Vector3 clusterCenter)
    {
        for (int j = 0; j < clusterSize; j++)
        {
            // Random position within the cluster radius
            Vector2 randomOffset2D = Random.insideUnitCircle * clusterRadius;
            Vector3 randomPosition = new Vector3(
                clusterCenter.x + randomOffset2D.x,
                clusterCenter.y, // Y will be adjusted after this
                clusterCenter.z + randomOffset2D.y
            );

            // Adjust Y position based on terrain height
            float terrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition);
            randomPosition.y = terrainHeight;

            // Instantiate the light_flower prefab
            GameObject newFlower = Instantiate(lightFlowerPrefab, randomPosition, Quaternion.identity);

            // Randomize the scale of the flower
            float randomScale = Random.Range(minScale, maxScale);
            newFlower.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            // Randomize the rotation of the flower
            newFlower.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            // Get the Point Light component and randomize the intensity
            Light pointLight = newFlower.GetComponentInChildren<Light>();
            if (pointLight != null)
            {
                pointLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            }
        }
    }
}

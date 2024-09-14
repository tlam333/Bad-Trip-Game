using UnityEngine;

public class LightFlowerSpawner : MonoBehaviour
{
    public GameObject lightFlowerPrefab; // Assign the light_flower prefab in the Inspector
    public int flowersInCenter = 10; // Number of flowers to spawn near the center
    public int flowersOutsideCenter = 20; // Number of flowers to spawn outside the center
    public float centerRadius = 50f; // Defines the radius of the center area
    public Vector2 mapBounds = new Vector2(500f, 500f); // Size of the map (X and Z boundaries)
    public float minLightIntensity = 1f; // Minimum intensity for the light
    public float maxLightIntensity = 4f; // Maximum intensity for the light

    void Start()
    {
        // Spawn flowers in the center of the map
        SpawnFlowersInCenter();
        
        // Spawn flowers outside the center area
        SpawnFlowersOutsideCenter();
    }

    void SpawnFlowersInCenter()
    {
        for (int i = 0; i < flowersInCenter; i++)
        {
            // Random position within the center radius
            Vector2 randomPosition2D = Random.insideUnitCircle * centerRadius;
            Vector3 randomPosition = new Vector3(randomPosition2D.x, 0, randomPosition2D.y);

            // Adjust Y position based on terrain height
            float terrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition);
            randomPosition.y = terrainHeight;

            // Instantiate the light_flower prefab
            GameObject newFlower = Instantiate(lightFlowerPrefab, randomPosition, Quaternion.identity);

            // Get the Point Light component and randomize the intensity
            Light pointLight = newFlower.GetComponentInChildren<Light>();
            if (pointLight != null)
            {
                pointLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            }
        }
    }

    void SpawnFlowersOutsideCenter()
    {
        for (int i = 0; i < flowersOutsideCenter; i++)
        {
            Vector3 randomPosition;

            do
            {
                // Random position within map bounds
                randomPosition = new Vector3(
                    Random.Range(-mapBounds.x, mapBounds.x),
                    0,
                    Random.Range(-mapBounds.y, mapBounds.y)
                );
            }
            while (randomPosition.magnitude <= centerRadius); // Ensure it's outside the center area

            // Adjust Y position based on terrain height
            float terrainHeight = Terrain.activeTerrain.SampleHeight(randomPosition);
            randomPosition.y = terrainHeight;

            // Instantiate the light_flower prefab
            GameObject newFlower = Instantiate(lightFlowerPrefab, randomPosition, Quaternion.identity);

            // Get the Point Light component and randomize the intensity
            Light pointLight = newFlower.GetComponentInChildren<Light>();
            if (pointLight != null)
            {
                pointLight.intensity = Random.Range(minLightIntensity, maxLightIntensity);
            }
        }
    }
}

using UnityEngine;

public class ChangeOpacity : MonoBehaviour
{
    // Reference to the object's Renderer
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component attached to the object
        objectRenderer = GetComponent<Renderer>();
        
        // Set initial opacity (0 = fully transparent, 1 = fully opaque)
        ChangeObjectOpacity(0.5f); // Example: 50% opacity
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            ChangeObjectOpacity(0.5f);
        } else if (Input.GetKeyUp(KeyCode.P)) {
            ChangeObjectOpacity(1f);
        }
    }

    // Function to change the opacity
    public void ChangeObjectOpacity(float opacity)
    {
        // Get the material's color
        Color objectColor = objectRenderer.material.color;

        // Set the alpha (opacity) to the specified value
        objectColor.a = opacity;

        // Apply the updated color to the material
        objectRenderer.material.color = objectColor;
    }
}

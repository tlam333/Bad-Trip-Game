using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Blur_manager : MonoBehaviour
{
    public PostProcessVolume p;
    private DepthOfField depthOfField;
    private int current_blur = 19;

    private float[] blurs = {0.1f, 0.5f, 0.7f, 1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.8f, 2f, 2.2f, 2.4f, 2.6f, 3f, 3.5f, 4.5f, 6f, 8f, 18f};

    void Start()
    {
        // Ensure the DepthOfField effect is correctly set up
        if (p == null)
        {
            Debug.LogError("PostProcessVolume is not assigned.");
            return;
        }

        if (!p.profile.TryGetSettings(out depthOfField))
        {
            Debug.LogError("DepthOfField effect not found in PostProcessVolume profile.");
            return;
        }

        // Initialize with the current blur level
        update_blur(current_blur);
    }

    public void update_blur(int blur_level)
    {
        current_blur = blur_level;

        if (current_blur == 19)
        {
            depthOfField.enabled.value = false;
        }
        else
        {
            depthOfField.enabled.value = true;
            depthOfField.focusDistance.value = blurs[current_blur];
        }

        Debug.Log("Blur updated: " + current_blur + " with focus distance: " + depthOfField.focusDistance.value);
    }
}

using UnityEngine;

public class FlashLight : ItemTest {

    public float originalIntensity = 4;
    private float lastUseTime;


    public override void Use(IntoxicationManager intoxicationManagerRef)
    {   
        float currTime = Time.time;

        
        Light spotLight = GetComponentInChildren<Light>();

        if (spotLight) {
            if (spotLight.intensity == originalIntensity) spotLight.intensity = 0; 
            else spotLight.intensity = originalIntensity;
        }
        }
}

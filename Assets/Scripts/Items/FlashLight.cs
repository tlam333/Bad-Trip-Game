using UnityEngine;

public class FlashLight : Item {

    private float originalIntensity = 7;
    private float coolDownTime = 0.1f;
    private float lastUseTime;


    public override void Use()
    {   
        float currTime = Time.time;

        // Limit light spam
        if (currTime - lastUseTime >= coolDownTime) {
            Light spotLight = GetComponentInChildren<Light>();

            if (spotLight) {
                if (spotLight.intensity == originalIntensity) spotLight.intensity = 0; 
                else spotLight.intensity = originalIntensity;
            }
        }
    }

    void Start() {
        lastUseTime = Time.time;
    }
}
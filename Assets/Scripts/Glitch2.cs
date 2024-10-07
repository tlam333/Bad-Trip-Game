using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch2 : MonoBehaviour
{
    public float glitchChance = 1f;

    private Renderer holoRenderer;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);

    void Awake() {
        holoRenderer = GetComponent<Renderer>();
    }

    IEnumerator Start() {
        while (true) {
            float glitchTest = Random.Range(0f, 1f);

            if (glitchTest <= glitchChance) {
                StartCoroutine (StartGlitch());
            }
            yield return glitchLoopWait;
        }
    }

    IEnumerator StartGlitch() {
        glitchDuration = new WaitForSeconds(Random.Range(.05f,.25f));
        holoRenderer.material.SetFloat("_Amount", .005f);
        // holoRenderer.material.SetColor("_TintColor", new Color32(7, 0, 115, 255)); // blue
        holoRenderer.material.SetColor("_TintColor", new Color32(115, 7, 0, 255)); // red
        holoRenderer.material.SetFloat("_CutOutThresh", .3f);
        holoRenderer.material.SetFloat("_Amplitude", Random.Range(100,250));
        holoRenderer.material.SetFloat("_Speed", Random.Range(1,10));
        yield return glitchDuration;
        holoRenderer.material.SetFloat("_Amount", 0f);
        holoRenderer.material.SetFloat("_CutOutThresh", 0f);
        holoRenderer.material.SetColor("_TintColor", new Color32(115, 101, 0, 255)); // yellow
    }
}

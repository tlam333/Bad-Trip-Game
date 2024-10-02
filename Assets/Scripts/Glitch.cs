using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitch : MonoBehaviour
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
        holoRenderer.material.SetFloat("_Amount", 1f);
        holoRenderer.material.SetFloat("_CutOutThresh", .71f);
        holoRenderer.material.SetFloat("_Amplitude", Random.Range(100,250));
        holoRenderer.material.SetFloat("_Speed", Random.Range(1,10));
        yield return glitchDuration;
        holoRenderer.material.SetFloat("_Amount", 0f);
        holoRenderer.material.SetFloat("_CutOutThresh", 0f);
    }
}

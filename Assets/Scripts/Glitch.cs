using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GlitchSpace {
    public class Glitch : MonoBehaviour
    {
        public float glitchChance = 0f;

        private Renderer holoRenderer;
        private WaitForSeconds glitchLoopWait = new WaitForSeconds(.1f);
        private WaitForSeconds glitchDuration = new WaitForSeconds(.1f);
        public IntoxicationManager IM;

        void Awake() {
            holoRenderer = GetComponent<Renderer>();
        }

        IEnumerator Start() {
            while (true) {
                Debug.Log(glitchChance);
                float glitchTest = Random.Range(0f, 1f);

                if (glitchTest <= glitchChance) {
                    StartCoroutine (StartGlitch());
                }
                yield return glitchLoopWait;
            }
        }

        public void Update(){
            Debug.Log("level:"+ IM.currentIntoxication);
            glitchChance = IM.currentIntoxication/1000f;
            Debug.Log(glitchChance);
        }

        IEnumerator StartGlitch() {
            glitchDuration = new WaitForSeconds(Random.Range(.05f,.25f));
            holoRenderer.material.SetFloat("_Amount", 1f);
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
}
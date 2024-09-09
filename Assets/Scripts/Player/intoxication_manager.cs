using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;

public class intoxication_manager : MonoBehaviour
{
    public int intox_level = 0;
    public int max_intox = 19;
    public Bar_behaviour bar;
    public Blur_manager BM;
    public float speed = 1;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            intox();
        }
    }

    private void intox(){
        intox_level = max_intox;
        BM.update_blur(intox_level);
        bar.set_max(max_intox);

        StartCoroutine(desintox()); 

    }

    private IEnumerator desintox() {
        speed = 2.5f;
        for (int i = 19; i > 0; i--) {
            yield return new WaitForSeconds(0.15f);
            intox_level--;
            BM.update_blur(19-intox_level);
            bar.set_value(intox_level);
        }
        speed = 1;
    }

}

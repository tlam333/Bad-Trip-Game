using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public int n_lives = 3;
    private static LifeCounter instance;

    // These three objects are cubes
    public GameObject GC_1;
    public GameObject GC_2;
    public GameObject GC_3;
 
    // These three objects are cubes
    public GameObject RC_1;
    public GameObject RC_2;
    public GameObject RC_3;
 
    // These three objects are lights
    public Light L1;
    public Light L2;
    public Light L3;
    public int[] doors_ids;

    System.Random random = new System.Random();

    public void Start() {
        doors_ids = door_randomizer();
    }

    public void Update() {
        update_rendering();
    }

    public static LifeCounter get_instance()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<LifeCounter>();
            if (instance == null)
            {
                GameObject singletonObject = new GameObject();
                instance = singletonObject.AddComponent<LifeCounter>();
                singletonObject.name = typeof(LifeCounter).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
        }
        return instance;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void update_rendering() {
        // Add checks to ensure objects are not null
        if (GC_1 == null || GC_2 == null || GC_3 == null ||
            RC_1 == null || RC_2 == null || RC_3 == null ||
            L1 == null || L2 == null || L3 == null)
        {
            Debug.Log("fuck");
            ReinitializeReferences();
        }

        switch (n_lives)
        {
            case 3:
                SetCubeVisibility(true, true, true, false, false, false);
                SetLightColor(Color.green, Color.green, Color.green);
                break;
            case 2:
                SetCubeVisibility(true, true, false, false, false, true);
                SetLightColor(Color.green, Color.green, Color.red);
                break;
            case 1:
                SetCubeVisibility(true, false, false, false, true, true);
                SetLightColor(Color.green, Color.red, Color.red);
                break;
            case 0:
                SetCubeVisibility(false, false, false, true, true, true);
                SetLightColor(Color.red, Color.red, Color.red);
                break;
        }
    }

    private void ReinitializeReferences()
    {
        // Try to find objects by name in the new scene, or reset if needed
        GC_1 = GameObject.Find("green-3");
        GC_2 = GameObject.Find("green-2");
        GC_3 = GameObject.Find("green-1");

        RC_1 = GameObject.Find("red-3");
        RC_2 = GameObject.Find("red-2");
        RC_3 = GameObject.Find("red-1");

        L1 = GameObject.Find("light-1").GetComponent<Light>();
        L2 = GameObject.Find("light-2").GetComponent<Light>();
        L3 = GameObject.Find("light-3").GetComponent<Light>();
    }

    public void die_once()
    {
        if (n_lives > 0) n_lives--;
    }

    private void SetCubeVisibility(bool gc1, bool gc2, bool gc3, bool rc1, bool rc2, bool rc3)
    {
        GC_1.SetActive(gc1);
        GC_2.SetActive(gc2);
        GC_3.SetActive(gc3);

        RC_1.SetActive(rc1);
        RC_2.SetActive(rc2);
        RC_3.SetActive(rc3);
    }

    private void SetLightColor(Color l1Color, Color l2Color, Color l3Color)
    {
        L1.color = l1Color;
        L2.color = l2Color;
        L3.color = l3Color;
    }

    private int[] door_randomizer() {
        if (doors_ids.Length > 0) return doors_ids;

        // returns an array of length 3 with the identifiers 0, 1, and 2 placed in a random order in the array
        int[] array = { 0, 1, 2 };

        // Shuffle the array
        for (int i = 0; i < array.Length; i++)
        {
            int j = random.Next(i, array.Length);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return array;
    }

    public int get_door_id(int door_index){
        if (doors_ids.Length == 0) doors_ids = door_randomizer();
        if (door_index == -1) return door_index;
        return doors_ids[door_index];
    }

}

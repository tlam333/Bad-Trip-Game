using System.Collections;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    // Reference to the objects involved in the dialog
    public GameObject fallObject;
    public GameObject lookDownObject;
    public GameObject nowObject;
    public GameObject floorObject;

    // Start is called before the first frame update
    void Start()
    {
        // Start the dialog sequence
        StartCoroutine(DialogSequence());
    }

    private IEnumerator DialogSequence()
    {
        // Wait 3 seconds
        // yield return new WaitForSeconds(6f);

        // // Make the object "fall" invisible
        // fallObject.SetActive(false);

        // // Wait 1 second
        // yield return new WaitForSeconds(1f);

        // // Make the object "look down" visible
        // lookDownObject.SetActive(true);

        // // Wait 2 seconds
        // yield return new WaitForSeconds(2f);

        // // Make the object "look down" invisible
        // lookDownObject.SetActive(false);

        // // Wait 1 second
        // yield return new WaitForSeconds(1f);

        // // Make the object "now" visible
        // nowObject.SetActive(true);
        // yield return new WaitForSeconds(2f);
        yield return new WaitForSeconds(0.5f);

        // Make the object "floor" disappear
        floorObject.SetActive(false);
    }
}

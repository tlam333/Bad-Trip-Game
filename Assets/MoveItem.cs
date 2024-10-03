using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform itemPosition;

    void Update() {
        transform.position = itemPosition.position;
    }
}

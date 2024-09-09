using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{

    private Rigidbody rb;
    private bool check;

    [SerializeField] float magnitude = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (check) {
            check = false;
            rb.AddForce(Vector3.up * magnitude);
        }
    }
}

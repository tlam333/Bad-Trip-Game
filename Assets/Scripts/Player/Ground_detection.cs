using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;

public class Ground_detection : MonoBehaviour
{
    
    public static bool grounded = true;
    HashSet<int> connected_grounds = new HashSet<int>();


    void Start()
    {
        
    }

    void Update()
    {
        check_ground();
    }

    // add the element id to the connected_grounds when it is considered as ground
    // remove the id when we leave it
    private void OnCollisionEnter(Collision collision)
    {
        int id = collision.transform.GetInstanceID();
        
        ContactPoint contact = collision.contacts[0];
        Vector3 collisionNormal = contact.normal;
        float angle = Vector3.Angle(transform.up, collisionNormal);

        if (angle <= 20f && !connected_grounds.Contains(id)) connected_grounds.Add(id);
    }

    private void OnCollisionExit(Collision collision)
    {
        int id = collision.transform.GetInstanceID();
        if (connected_grounds.Contains(id)) connected_grounds.Remove(id);
    }

    private void check_ground() {
        grounded = connected_grounds.Count > 0;
    }
}

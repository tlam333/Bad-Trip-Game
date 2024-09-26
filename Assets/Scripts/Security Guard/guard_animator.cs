using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guard_animator : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Start walking animation by default
        animator.SetBool("is_walking", true);
    }

    // Call this method to play the dying animation
    public void Die()
{
    
    animator.SetBool("is_dying", true);
    //animator.SetBool("is_walking", false);
    // Disable the movement script
    //GetComponent<GuardPatrol>().enabled = false;
}

}

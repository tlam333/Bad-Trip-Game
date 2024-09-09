using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_state_controller : MonoBehaviour
{

    Animator animator;
    private Rigidbody rb;
    private bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Ground_detection.grounded;
        make_movement(Input.GetKey(KeyCode.W), "isWalking");
        make_movement(Input.GetKey(KeyCode.S), "isBacking");
        make_movement(Input.GetKey(KeyCode.A), "isLefting");
        make_movement(Input.GetKey(KeyCode.D), "isRighting");
        sprint_detector();
        jump_detector();

    }

    void make_movement(bool pressed, String transition){
        bool doing = animator.GetBool(transition);
        if (pressed) {
            if (!doing && isGrounded)animator.SetBool(transition, true); // start animation
        } else {
            if (doing) animator.SetBool(transition, false); // stop animation
        }
    }

    void sprint_detector() {
        bool running = animator.GetBool("isRunning");
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (! Input.GetKey(KeyCode.W)) return;

            if (!running) {
                animator.SetBool("isRunning", true); // start animation
            } 
        } else {
            if (running) animator.SetBool("isRunning", false); // stop animation
        }
    }

    void jump_detector() {

        bool pressed = Input.GetKey(KeyCode.Space);
        bool jumping = animator.GetBool("isJumping");
        if (pressed && !jumping) {
            animator.SetBool("isJumping", true);
        } 
        if (!pressed && jumping) {
            // animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("isJumping", false);
    }

}

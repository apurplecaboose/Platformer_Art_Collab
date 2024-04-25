using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animation : MonoBehaviour
{
    public bool IsPlayRun, IsPlayIdle, IsPlayFall, IsPlayFire, IsPlayJump,IsGrounded;
    Animator p_animation;
    private void Awake()
    {
        p_animation= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayRun&& IsGrounded)
        {
            Run();
        }
        if (IsPlayJump)
        {
            Jump();
        }
        if (IsPlayIdle)
        {
            Idle();
        }
        if (IsPlayFall)
        {
            Falling();
        }
    }
    void Run()
    {
        p_animation.SetBool("IsRunning", true);
        p_animation.SetBool("IsIdle", false);
        p_animation.SetBool("IsGrounded", true);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsFalling", false);
    }
    void Jump()
    {
        p_animation.SetBool("IsJumping", true);
        p_animation.SetBool("IsIdle", false);
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsGrounded", false);
        p_animation.SetBool("IsFalling", false);
    }
    void Idle()
    {
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsIdle", true);
        p_animation.SetBool("IsGrounded", true);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsFalling", false);
    }

    void Falling()
    {
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsGrounded", false);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsFalling", true);
        p_animation.SetBool("IsIdle", false);
    }


}

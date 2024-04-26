using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animation : MonoBehaviour
{
    public bool IsPlayRun, IsPlayIdle, IsPlayFire, IsPlayJump;
    Animator p_animation;
    private void Awake()
    {
        p_animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayRun)
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
        if (IsPlayFire && IsPlayRun)
        {
            Fire();
        }
        if (IsPlayFire && IsPlayJump)
        {
            Fire();
        }
        if (IsPlayFire && IsPlayIdle)
        {
            Fire();
        }

    }
    void Run()
    {
        p_animation.SetBool("IsRunning", true);
        p_animation.SetBool("IsIdle", false);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsFiring", false);
    }
    void Jump()
    {
        p_animation.SetBool("IsJumping", true);
        p_animation.SetBool("IsIdle", false);
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsFiring", false);

    }
    void Idle()
    {
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsIdle", true);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsFiring", false);

    }

    void Fire()
    {
        p_animation.SetBool("IsRunning", false);
        p_animation.SetBool("IsJumping", false);
        p_animation.SetBool("IsIdle", false);
        p_animation.SetBool("IsFiring", true);
    }


}

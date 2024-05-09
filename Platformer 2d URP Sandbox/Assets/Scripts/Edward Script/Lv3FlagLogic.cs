using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;

public class Lv3FlagLogic : MonoBehaviour
{
    [SerializeField] SceneEnum.SceneList _TargetScene;
    public TransitionProfile TransitionProfile;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.P_state = GameManager.PlayerState.Win;//J:limit player movement
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;

            Invoke("TriggerTransition", .35f);
        }
    }
    void TriggerTransition()
    {
        TransitionAnimator.Start(TransitionProfile, false, 0, _TargetScene.ToString());
    }
}

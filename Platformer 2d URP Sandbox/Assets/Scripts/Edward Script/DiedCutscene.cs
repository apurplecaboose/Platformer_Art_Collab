using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;

public class DiedCutscene : MonoBehaviour
{
    float CutsceneTimer = 2f;
    bool  _trigger;
    public TransitionProfile TransitionProfile;

    private void Awake()
    {
        Time.timeScale = 1; // fix slowmo death bug
        Time.fixedDeltaTime = 0.02f;
    }
    void Update()
    {
        CutsceneTimer -= Time.unscaledDeltaTime;
        if (CutsceneTimer < 0 || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {
            //Switch Scene
            if (!_trigger)
            {
                TransitionAnimator.Start(TransitionProfile, false, 0, GameManager.SceneCache.ToString());
                _trigger = true;
            }
        }
    }
}

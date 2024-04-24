using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;

public class DiedCutscene : MonoBehaviour
{
    float CutsceneTimer = 3;
    bool  _trigger;
    public TransitionProfile TransitionProfile;

    void Update()
    {
        CutsceneTimer -= Time.deltaTime;
        if (CutsceneTimer < 0 || Input.GetKeyDown(KeyCode.Space))
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

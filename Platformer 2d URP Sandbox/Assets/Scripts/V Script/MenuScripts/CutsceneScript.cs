using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public string TargetSceneName;
    public VideoPlayer myVideoplayer;
    public Object TargetScene;
    public GameObject TransitionCompenent;
    private void Start()
    {
        
        myVideoplayer.loopPointReached += SwitchSceneOnFinish;
        TransitionCompenent.GetComponent<TransitionAnimator>().sceneNameToLoad = TargetScene.name;
    }
    void SwitchSceneOnFinish(VideoPlayer vp)
    {
        
        TransitionCompenent.SetActive(true);
        Debug.Log("requirements reached");
    }
}

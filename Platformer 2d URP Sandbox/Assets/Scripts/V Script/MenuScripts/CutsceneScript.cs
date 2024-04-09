using System.Collections;
using System.Collections.Generic;
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
    }
    void SwitchSceneOnFinish(VideoPlayer vp)
    {
        
        TransitionCompenent.SetActive(true);
        Debug.Log("requirements reached");
    }
}

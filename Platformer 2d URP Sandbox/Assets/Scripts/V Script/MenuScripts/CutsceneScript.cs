using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public string TargetSceneName;
    public VideoPlayer myVideoplayer;
    private void Start()
    {
        
        myVideoplayer.loopPointReached += SwitchSceneOnFinish;
    }
    void SwitchSceneOnFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene(TargetSceneName);
        Debug.Log("requirements reached");
    }
}

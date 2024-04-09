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
    private void Start()
    {
        
        myVideoplayer.loopPointReached += SwitchSceneOnFinish;
    }
    void SwitchSceneOnFinish(VideoPlayer vp)
    {
        SceneManager.LoadScene(TargetScene.name);
        Debug.Log("requirements reached");
    }
}

using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public string TargetSceneName;
    public VideoPlayer CutscenePlayer;
    public Object TargetScene;
    public GameObject TransitionCompenent;
    float _cutsceneLength;
    float _cutsceneTimer;
    [SerializeField] float _videoOffset;
    private void Start()
    {
        _cutsceneLength = (float)CutscenePlayer.length;
        if (_cutsceneTimer < _videoOffset)
        {
            Debug.Log("Offset is larger than video length u idiot");
        }
        TransitionCompenent.GetComponent<TransitionAnimator>().sceneNameToLoad = TargetScene.name;
    }
    private void Update()
    {
        _cutsceneTimer += Time.deltaTime;
        if(_cutsceneTimer >= _cutsceneLength - _videoOffset)
        {
            TransitionCompenent.SetActive(true);
        }
    }
    //void SwitchSceneOnFinish(VideoPlayer vp)
    //{
        
    //    TransitionCompenent.SetActive(true);
    //    Debug.Log("requirements reached");
    //}
}

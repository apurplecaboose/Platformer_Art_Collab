using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneScript : MonoBehaviour
{
    public VideoPlayer CutscenePlayer;
    [SerializeField] SceneEnum.SceneList _TargetScene;
    public TransitionProfile TransitionProfile;
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
    }
    private void Update()
    {
        _cutsceneTimer += Time.deltaTime;
        if(_cutsceneTimer >= _cutsceneLength - _videoOffset)
        {
            TransitionAnimator.Start(TransitionProfile, false, 0, _TargetScene.ToString(), LoadSceneMode.Single);
        }
    }
}
